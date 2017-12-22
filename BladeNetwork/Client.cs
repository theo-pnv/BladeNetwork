using System.Diagnostics;
using System.Net.Sockets;

namespace blade
{

	/// <summary>
	/// This is the client class.
	/// </summary>
	public class Client : ATcpObject
	{
		TcpClient client;
		NetworkStream stream;

		public Client(string ip, int port, Queue queue) :
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

		~Client()
		{
			stream.Close();
			client.Close();
		}

	}
}
