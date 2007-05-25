using System.Windows.Threading ;
using agsXMPP.protocol.client ;
using agsXMPP.protocol.iq.disco ;
using agsXMPP.protocol.x.data ;
using agsXMPP.Xml.Dom ;
using xeus2.Properties ;
using xeus2.xeus.Utilities ;

namespace xeus2.xeus.Core
{
	internal class Services : ObservableCollectionDisp< Service >
	{
		private delegate void ServiceItemCallback( DiscoItem discoItem, DiscoItem parent ) ;

		private delegate void ServiceItemInfoCallback( DiscoItem discoItem, DiscoInfo info ) ;

		private delegate void OnServiceItemErrorCallback( IQ iq ) ;

		private delegate void OnCommandsItemInfoCallback( DiscoItem discoItem, IQ iq ) ;

		private static Services _instance = new Services() ;

		public static Services Instance
		{
			get
			{
				return _instance ;
			}
		}

		public static ServiceCategories Categories
		{
			get
			{
				return _categories ;
			}
		}

		private static ServiceCategories _categories = new ServiceCategories();

		public void OnCommandsItemInfo( object sender, DiscoItem discoItem, IQ iq )
		{
			App.InvokeSafe( DispatcherPriority.Background,
			                new OnCommandsItemInfoCallback( OnCommandsItemInfo ), discoItem, iq ) ;
		}
		
		public void OnServiceItemInfo( object sender, DiscoItem discoItem, DiscoInfo info )
		{
			App.InvokeSafe( DispatcherPriority.Background,
			                new ServiceItemInfoCallback( OnServiceItemInfo ), discoItem, info ) ;
		}

		public void OnServiceItem( object sender, DiscoItem discoItem, DiscoItem parent )
		{
			App.InvokeSafe( DispatcherPriority.Background,
			                new ServiceItemCallback( OnServiceItem ), discoItem, parent ) ;
		}

		public void OnServiceItemError( object sender, IQ iq )
		{
			App.InvokeSafe( DispatcherPriority.Background,
			                new OnServiceItemErrorCallback( OnServiceItemError ), iq ) ;
		}

		public void OnServiceItemError( IQ iq )
		{
			EventInfo eventInfo = new EventInfo( string.Format( Resources.Error_ServiceDiscoFailed, iq.From, iq.Error.Code ) ) ;
			Events.Instance.OnEvent( eventInfo ) ;
		}

		public void OnCommandsItemInfo( DiscoItem discoItem, IQ iq )
		{
			foreach ( Node node in iq.Query.ChildNodes )
			{
				DiscoItem item = node as DiscoItem ;

				if ( item != null )
				{
					lock ( _syncObject )
					{
						Service service = FindService( item ) ;

						if ( service != null )
						{
							service.IsCommand = true ;
						}
					}
				}
			}
		}

		public void OnServiceItemInfo( DiscoItem discoItem, DiscoInfo info )
		{
			lock ( _syncObject )
			{
				Service service = FindService( discoItem ) ;

				if ( service != null )
				{
					service.DiscoInfo = info ;

					if ( service.IsToplevel )
					{
						_categories.AddService( service ) ;
					}
				}
			}
		}

		public void OnServiceItem( DiscoItem discoItem, DiscoItem parent )
		{
			lock ( _syncObject )
			{
				Service service = FindService( discoItem ) ;
				Service parentService = null ;

				if ( parent != null )
				{
					parentService = FindService( parent ) ;
				}

				if ( service == null )
				{
					if ( parent == null )
					{
						Service newService = new Service( discoItem, true ) ;
						Add( newService ) ;
					}
					else
					{
						lock ( parentService.Services._syncObject )
						{
							parentService.Services.Add( new Service( discoItem, false ) ) ;
						}
					}
				}
			}
		}

		// unsafe, lock when calling
		public Service FindService( DiscoItem discoItem )
		{
			foreach ( Service item in Items )
			{
				if ( JidUtil.CompareDiscoItem( item.DiscoItem, discoItem ) )
				{
					return item ;
				}

				lock ( item.Services._syncObject )
				{
					Service service = item.Services.FindService( discoItem ) ;

					if ( service != null )
					{
						return service ;
					}
				}
			}

			return null ;
		}
	}
}