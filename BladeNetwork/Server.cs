using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace VRC.Common.Networking
{
	/// <summary>
	/// Server class.
	/// </summary>
	public class Server : ATcpObject
	{
		TcpListener listener;
		readonly Object connectionListLocker = new Object();
		List<ClientConnection> connectionList;
		Action<object, ClientConnectionEventArgs> newMsgCallback;

		public Server(string ip, int port, Action<object, ClientConnectionEventArgs> nMC) :
			base()
		{
			newMsgCallback = nMC;

			connectionList = new List<ClientConnection>();
			IPAddress ipAddress = IPAddress.Parse(ip);
			listener = new TcpListener(ipAddress, port);
			Trace.WriteLine("New Server listening on " + ip + ":" + port);
			listener.Start();

			AsyncAcceptTcpClient();
		}

		public void AsyncAcceptTcpClient()
		{
			listener.BeginAcceptTcpClient(DoAcceptTcpClientCallback, null);
		}

		private void DoAcceptTcpClientCallback(IAsyncResult ar)
		{
			listener.BeginAcceptTcpClient(DoAcceptTcpClientCallback, null);

			Trace.WriteLine("Client connected !");
			TcpClient client = listener.EndAcceptTcpClient(ar);
			Queue queue = new Queue();

			ClientConnection connection = new ClientConnection(client, queue);
			connection.e += newMsgCallback.Invoke;

			ConnectionList.Add(connection);

			AsyncRead(client.GetStream(), queue);
		}

		// Override of this method so that we can handle the disconnection of a client
		protected override void EndReadMessage(IAsyncResult result)
		{
			AsyncState state = (AsyncState)result.AsyncState;
			int len = state._stream.EndRead(result);

			string str = System.Text.Encoding.Default.GetString(state._data);

			// If stream is 0 bytes long, the client disconnected.
			// We can remove it from the list.
			if (string.IsNullOrEmpty(str)) {
				for (int i = connectionList.Count - 1; i >= 0; i--) {
					// Using HashCode as an ID : Good idea or not ?
					if (connectionList[i].Client.GetStream().GetHashCode() == state._stream.GetHashCode()) {
						connectionList.Remove(connectionList[i]);
						return;
					}
				}
			}

			Trace.WriteLine("Received: " + str);
			state._queue.EnqueueEvent(str);

			BeginReadSize(state._stream, state._queue);
		}

		public List<ClientConnection> ConnectionList
		{
			get
			{
				lock (connectionListLocker) {
					return connectionList;
				}
			}
			set
			{
				lock (connectionListLocker) {
					connectionList = value;
				}
			}
		}

	}
}
