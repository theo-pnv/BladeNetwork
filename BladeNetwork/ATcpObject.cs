using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace blade
{
	/// <summary>
	/// Implementation of basic methods to send and receive messages through a TCP based communication.
	/// This is the base class, used for the server as well as for the client.
	/// Communication is done asynchronously.
	/// A queue is filled with received messages for subsequent processing.
	/// </summary>
	public abstract class ATcpObject
	{

		protected struct AsyncState
		{
			public AsyncState(byte[] data, NetworkStream stream, Queue queue)
			{
				_data = data;
				_stream = stream;
				_queue = queue;
			}
			public byte[] _data;
			public NetworkStream _stream;
			public Queue _queue;
		}

		#region Public Methods

		public void AsyncSend(NetworkStream stream, string msg)
		{
			BeginSend(stream, msg);
		}

		public void AsyncRead(NetworkStream stream, Queue queue)
		{
			BeginReadSize(stream, queue);
		}

		#endregion

		#region Internal Methods

		private void BeginSend(NetworkStream stream, string msg)
		{
			byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
			var bLength = BitConverter.GetBytes(data.Length);
			var bFull = bLength.Concat(data).ToArray();

			Trace.WriteLine("Sending: " + Encoding.ASCII.GetString(data));

			try {
				stream.BeginWrite(bFull, 0, bFull.Length, EndSend, bFull);
			}
			catch (Exception e) {
				Trace.WriteLine(e.Message);
			}
		}

		private void EndSend(IAsyncResult result)
		{
			var bytes = (byte[])result.AsyncState;

		}

		protected void BeginReadSize(NetworkStream stream, Queue queue)
		{
			int intSize = sizeof(int);
			byte[] bDataLength = new byte[intSize];
			AsyncState state = new AsyncState(bDataLength, stream, queue);

			stream.BeginRead(bDataLength, 0, bDataLength.Length, EndReadSize, state);
		}

		private void EndReadSize(IAsyncResult result)
		{
			AsyncState state = (AsyncState)result.AsyncState;

			try {
				int len = state._stream.EndRead(result);
			}
			catch (System.IO.IOException) {
				Trace.WriteLine("The network stream is not available anymore. A client may have been disconnected.");
				return;
			}
			int dataLength = BitConverter.ToInt32(state._data, 0);

			BeginReadMessage(dataLength, state._stream, state._queue);
		}

		private void BeginReadMessage(int len, NetworkStream stream, Queue queue)
		{
			byte[] bData = new byte[len];
			AsyncState state = new AsyncState(bData, stream, queue);

			stream.BeginRead(bData, 0, bData.Length, EndReadMessage, state);
		}

		protected virtual void EndReadMessage(IAsyncResult result)
		{
			AsyncState state = (AsyncState)result.AsyncState;
			int len = state._stream.EndRead(result);

			string str = System.Text.Encoding.Default.GetString(state._data);

			Trace.WriteLine("Received: " + str);
			state._queue.EnqueueEvent(str);

			BeginReadSize(state._stream, state._queue);
		}

		#endregion
	}
}
