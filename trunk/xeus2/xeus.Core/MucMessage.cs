using agsXMPP.protocol.client ;

namespace xeus2.xeus.Core
{
	internal class MucMessage
	{
		private readonly Message _message ;
		private readonly MucContact _sender ;

		public MucMessage( Message message, MucContact sender )
		{
			_message = message ;
			_sender = sender ;
		}

		public string Body
		{
			get
			{
				return _message.Body ;
			}
		}

		public MucContact Sender
		{
			get
			{
				return _sender ;
			}
		}
	}
}