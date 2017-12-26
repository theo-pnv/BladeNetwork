using System;
using System.Net.Sockets;

namespace blade
{
	public class ClientConnectionEventArgs : EventArgs
	{
		public ClientConnection clientConnection;

		public ClientConnectionEventArgs(ClientConnection cC)
		{
			clientConnection = cC;
		}
	}

	/// <summary>
	/// This class represents a pair : a client and its associated queue of messages.
	/// This way we know precisely who sent the last enqueued message among the list of clients.
	/// </summary>
	public class ClientConnection
	{
		TcpClient client;
		Queue queue;
		public event EventHandler<ClientConnectionEventArgs> e;

		#region Accessors

		public TcpClient Client
		{
			get { return client; }
		}

		public Queue Queue
		{
			get { return queue; }
		}

		#endregion

		public ClientConnection(TcpClient c, Queue q)
		{
			client = c;
			queue = q;
			queue.e += OnNewMsg;
		}

		public void OnNewMsg(object sender, QueueEventArgs eventArgs)
		{
			e?.Invoke(this, new ClientConnectionEventArgs(this));
		}
	}
}
