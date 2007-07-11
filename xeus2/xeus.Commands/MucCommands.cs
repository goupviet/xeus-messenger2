using System.Windows ;
using System.Windows.Input ;
using xeus2.xeus.Core;

namespace xeus2.xeus.Commands
{
	public static class MucCommands
	{
		private static RoutedUICommand _changeStatus =
			new RoutedUICommand( "Change Status", "ChangeStatus", typeof ( MucCommands ) ) ;

		private static RoutedUICommand _changeNick =
			new RoutedUICommand( "Change Nickname", "ChangeNickmane", typeof ( MucCommands ) ) ;

		private static RoutedUICommand _sendPrivateMessage =
			new RoutedUICommand( "Send Private Message", "SendPrivateMessage", typeof ( MucCommands ) ) ;

		private static RoutedUICommand _invite =
			new RoutedUICommand( "Invite", "Invite", typeof ( MucCommands ) ) ;

		private static RoutedUICommand _sendMessageToAll =
			new RoutedUICommand( "Send Message To All", "SendMessageToAll", typeof ( MucCommands ) ) ;

		private static RoutedUICommand _modifySubject =
			new RoutedUICommand( "Modify Subject", "ModifySubject", typeof ( MucCommands ) ) ;

		private static RoutedUICommand _grantVoice =
			new RoutedUICommand( "Grant Voice", "GrantVoice", typeof ( MucCommands ) ) ;

		private static RoutedUICommand _revokeVoice =
			new RoutedUICommand( "Revoke Voice", "RevokeVoice", typeof ( MucCommands ) ) ;

		public static RoutedUICommand ChangeStatus
		{
			get
			{
				return _changeStatus ;
			}
		}

		public static RoutedUICommand ChangeNick
		{
			get
			{
				return _changeNick ;
			}
		}

		public static RoutedUICommand SendPrivateMessage
		{
			get
			{
				return _sendPrivateMessage ;
			}
		}

		public static RoutedUICommand Invite
		{
			get
			{
				return _invite ;
			}
		}

		public static RoutedUICommand SendMessageToAll
		{
			get
			{
				return _sendMessageToAll ;
			}
		}

		public static RoutedUICommand ModifySubject
		{
			get
			{
				return _modifySubject ;
			}
		}

		public static RoutedUICommand GrantVoice
		{
			get
			{
				return _grantVoice ;
			}
		}

		public static RoutedUICommand RevokeVoice
		{
			get
			{
				return _revokeVoice ;
			}
		}

		public static void RegisterCommands( Window window )
		{
			window.CommandBindings.Add(
				new CommandBinding( _changeStatus, ExecuteChangeStatus, CanExecuteChangeStatus ) ) ;

			window.CommandBindings.Add(
				new CommandBinding( _changeNick, ExecuteChangeNick, CanExecuteChangeNick ) ) ;

			window.CommandBindings.Add(
				new CommandBinding( _sendPrivateMessage, ExecuteSendPrivateMessage, CanExecuteSendPrivateMessage ) ) ;

			window.CommandBindings.Add(
				new CommandBinding( _invite, ExecuteInvite, CanExecuteInvite ) ) ;

			window.CommandBindings.Add(
				new CommandBinding( _sendMessageToAll, ExecuteSendMessageToAll, CanExecuteSendMessageToAll ) ) ;

			window.CommandBindings.Add(
				new CommandBinding( _modifySubject, ExecuteModifySubject, CanExecuteModifySubject ) ) ;

			window.CommandBindings.Add(
				new CommandBinding( _grantVoice, ExecuteGrantVoice, CanExecuteGrantVoice ) ) ;

			window.CommandBindings.Add(
				new CommandBinding( _revokeVoice, ExecuteRevokeVoice, CanExecuteRevokeVoice ) ) ;
		}

		public static void CanExecuteChangeStatus( object sender, CanExecuteRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;

			e.Handled = true ;
			//e.CanExecute = ( service != null && service.IsRegistrable ) ;
		}

		public static void ExecuteChangeStatus( object sender, ExecutedRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;
			//Account.Instance.GetService( service ) ;

			e.Handled = true ;
		}

		public static void CanExecuteChangeNick( object sender, CanExecuteRoutedEventArgs e )
		{
            MucContact mucContact = e.Parameter as MucContact;

            e.Handled = true;
            e.CanExecute = (mucContact.Nick == mucContact.MucRoom.Nick);
		}

		public static void ExecuteChangeNick( object sender, ExecutedRoutedEventArgs e )
		{
            MucContact mucContact = e.Parameter as MucContact;

			e.Handled = true ;
		}

		public static void CanExecuteSendPrivateMessage( object sender, CanExecuteRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;

			e.Handled = true ;
			//e.CanExecute = ( service != null && service.IsRegistrable ) ;
		}

		public static void ExecuteSendPrivateMessage( object sender, ExecutedRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;
			//Account.Instance.GetService( service ) ;

			e.Handled = true ;
		}

		public static void CanExecuteInvite( object sender, CanExecuteRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;

			e.Handled = true ;
			//e.CanExecute = ( service != null && service.IsRegistrable ) ;
		}

		public static void ExecuteInvite( object sender, ExecutedRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;
			//Account.Instance.GetService( service ) ;

			e.Handled = true ;
		}

		public static void CanExecuteSendMessageToAll( object sender, CanExecuteRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;

			e.Handled = true ;
			//e.CanExecute = ( service != null && service.IsRegistrable ) ;
		}

		public static void ExecuteSendMessageToAll( object sender, ExecutedRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;
			//Account.Instance.GetService( service ) ;

			e.Handled = true ;
		}

		public static void CanExecuteModifySubject( object sender, CanExecuteRoutedEventArgs e )
		{
			e.Handled = true ;
			e.CanExecute = true ;
		}

		public static void ExecuteModifySubject( object sender, ExecutedRoutedEventArgs e )
		{
		    MucRoom mucRoom = e.Parameter as MucRoom;

            if (mucRoom != null)
            {
                Middle.ChangeMucTopic.Instance.DisplayTopic(mucRoom);
            }

			e.Handled = true ;
		}

		public static void CanExecuteGrantVoice( object sender, CanExecuteRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;

			e.Handled = true ;
			//e.CanExecute = ( service != null && service.IsRegistrable ) ;
		}

		public static void ExecuteGrantVoice( object sender, ExecutedRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;
			//Account.Instance.GetService( service ) ;

			e.Handled = true ;
		}

		public static void CanExecuteRevokeVoice( object sender, CanExecuteRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;

			e.Handled = true ;
			//e.CanExecute = ( service != null && service.IsRegistrable ) ;
		}

		public static void ExecuteRevokeVoice( object sender, ExecutedRoutedEventArgs e )
		{
			//Service service = e.Parameter as Service ;
			//Account.Instance.GetService( service ) ;

			e.Handled = true ;
		}
	}
}