using System.Diagnostics;
using System.Net.Sockets;

namespace VRC.Common.Networking
{

	/// <summary>
	/// This is the client class.
	/// </summary>
	public class NetworkingClient : ATcpObject
	{
		TcpClient client;
		NetworkStream stream;

		public NetworkingClient(string ip, int port, Queue queue) :
			base()
		{
			client = new TcpClient(ip, port);
			stream = client.GetStream();
			Trace.WriteLine("New Client connected to " + ip + ":" + port);

			AsyncRead(stream, queue);
		}

		public void Send(string msg)
		{
			AsyncSend(stream, msg);
		}

		~NetworkingClient()
		{
			stream.Close();
			client.Close();
		}

	}
}
