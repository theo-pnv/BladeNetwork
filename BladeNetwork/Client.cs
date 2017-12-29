using System;

namespace blade
{
	/// <summary>
	/// Client class.
	/// </summary>
	public class Client
	{
		private NetworkingClient client;
		private Queue queue;

		public Client(string ip, int port, Action<object, QueueEventArgs> msgHandler)
		{
			queue = new Queue();
			queue.e += msgHandler.Invoke;
			client = new NetworkingClient(ip, port, queue);
		}

		/// <summary>
		/// Used to send a message to the server.
		/// </summary>
		/// <param name="msg"></param>
		public void Send(string msg)
		{
			client.Send(msg);
		}
    
        public Queue Queue
        {
            get { return queue; }
            set { queue = value; }
        }

	}
}
