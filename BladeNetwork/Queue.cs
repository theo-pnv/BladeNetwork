using System;
using System.Collections.Generic;

namespace blade
{

	public class QueueEventArgs : EventArgs
	{
		public string data;
		public QueueEventArgs(string d)
		{
			data = d;
		}
	}


	/// <summary>
	/// This is an override of the C# queue object.
	/// It posesses an event handler, which is called each time a message is received,
	/// to process it correctly (This observer pattern avoids to loop on the queue
	/// to check if a message was enqueued at each processor tick).
	/// 
	/// Do not forget to set this event handler after creating the queue !
	/// </summary>
	public class Queue
	{
		readonly Object locker = new Object();
		Queue<string> data = new Queue<string>();

		public event EventHandler<QueueEventArgs> e;

		public Queue<string> Data
		{
			get
			{
				lock (locker) {
					return data;
				}

			}
		}

		public void EnqueueEvent(string str)
		{
			Data.Enqueue(str);
			e?.Invoke(this, new QueueEventArgs(str));
		}

	}
}
