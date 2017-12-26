namespace blade
{
	public class Notifier<Args>
	{
		public delegate void CallbackDelegate(Args eventArgs);

		private event CallbackDelegate _callbackEventList;

		public void Notify(Args eventArgs)
		{
			_callbackEventList?.Invoke(eventArgs);
		}

		public void Add(CallbackDelegate callback)
		{
			_callbackEventList += callback;
		}

		public void Remove(CallbackDelegate callback)
		{
			_callbackEventList -= callback;
		}
	}
}
