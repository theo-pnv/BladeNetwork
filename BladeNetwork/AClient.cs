namespace blade
{
	/// <summary>
	/// Abstract class representing a client.
	/// Every program acting as a client should inherit from it.
	/// They must implement a ReceiveHandler class that will allow them
	/// to parse the message queue in order to process received commands.
	/// </summary>
	public abstract class AClient
	{
		protected NetworkingClient client;
		protected Queue queue;

		public AClient(string ip, int port)
		{
			queue = new Queue();
			queue.e += MsgHandler;
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

		/// <summary>
		/// This method will be called every time a message is received
		/// (Pattern Observer)
		/// You should add your logic in its implementation.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public abstract void MsgHandler(object sender, QueueEventArgs e);
	}
}
