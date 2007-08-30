using System ;
using System.Collections;
using System.Collections.Generic ;
using System.Threading;
using System.Timers ;
using agsXMPP ;
using agsXMPP.net ;
using agsXMPP.protocol.client ;
using agsXMPP.protocol.extensions.commands ;
using agsXMPP.protocol.iq.disco ;
using agsXMPP.protocol.iq.register ;
using agsXMPP.protocol.iq.roster ;
using agsXMPP.protocol.iq.search ;
using agsXMPP.protocol.x.data ;
using agsXMPP.protocol.x.muc ;
using agsXMPP.protocol.x.muc.iq.owner;
using agsXMPP.Xml.Dom ;
using xeus2.Properties ;
using xeus2.xeus.Core.xeus.Data;
using xeus2.xeus.Middle ;
using Search=xeus2.xeus.Middle.Search;
using Timer=System.Timers.Timer;
using Uri=agsXMPP.Uri;

namespace xeus2.xeus.Core
{
	internal class DiscoverySessionData
	{
		private string _sessionKey ;
		private object _data ;

		public DiscoverySessionData( object data )
		{
			_data = data ;
			_sessionKey = Services.Instance.SessionKey ;
		}

		public string SessionKey
		{
			get
			{
				return _sessionKey ;
			}
		}

		public object Data
		{
			get
			{
				return _data ;
			}
		}
	}

	internal class Account : NotifyInfoDispatcher
	{
		private Timer _discoTime = new Timer( 250 ) ;

		private static Account _instance = new Account() ;

		private XmppClientConnection _xmppConnection = new XmppClientConnection() ;
		private DiscoManager _discoManager ;

		private MucManager _mucManager = null ;
		private bool _isLogged = false ;

		public static Account Instance
		{
			get
			{
				return _instance ;
			}
		}

		protected void Cleanup()
		{
			Services.Instance.Clear();
			Roster.Instance.Items.Clear();
		}

		public void Login()
		{
			Close() ;
			Cleanup() ;
			Open() ;
		}

		public void Open()
		{
			_discoManager = new DiscoManager( XmppConnection ) ;

			XmppConnection.UseCompression = true ;
			XmppConnection.Priority = Settings.Default.XmppPriority ;
			XmppConnection.AutoResolveConnectServer = true ;

			XmppConnection.ConnectServer = null ;
			XmppConnection.Resource = Settings.Default.XmppResource ;
			XmppConnection.SocketConnectionType = SocketConnectionType.Direct ;
			XmppConnection.UseStartTLS = true ;

			XmppConnection.AutoRoster = true ;
			XmppConnection.AutoAgents = true ;

			XmppConnection.Username = Settings.Default.XmppUserName ;
			XmppConnection.Password = Settings.Default.XmppPassword ;
			XmppConnection.Server = Settings.Default.XmppServer ;

			XmppConnection.OnClose += new ObjectHandler( _xmppConnection_OnClose ) ;
			XmppConnection.OnLogin += new ObjectHandler( _xmppConnection_OnLogin ) ;
			XmppConnection.OnRosterItem += new XmppClientConnection.RosterHandler( _xmppConnection_OnRosterItem ) ;
			XmppConnection.OnRosterEnd += new ObjectHandler( _xmppConnection_OnRosterEnd ) ;
			XmppConnection.OnPresence += new PresenceHandler( _xmppConnection_OnPresence ) ;
			XmppConnection.OnError += new ErrorHandler( _xmppConnection_OnError ) ;
			XmppConnection.OnAuthError += new XmppElementHandler( _xmppConnection_OnAuthError ) ;

			XmppConnection.OnIq += new IqHandler( _xmppConnection_OnIq ) ;

			Settings.Default.Save() ;

			_mucManager = new MucManager( XmppConnection ) ;

			XmppConnection.Open() ;

			_discoTime.Elapsed += new ElapsedEventHandler( _discoTime_Elapsed ) ;

		    _discoTime.AutoReset = false;
            _discoTime.Start();
		}

        private ArrayList _pendingDiscoInfo = ArrayList.Synchronized(new ArrayList());
        private ArrayList _pendingCommand = ArrayList.Synchronized(new ArrayList());

		private int _servicesCount ;
		private int _servicesDoneCount ;

        object _discoLock = new object();
	    private volatile bool _working = false;

		private void _discoTime_Elapsed( object sender, ElapsedEventArgs e )
		{
            if (_working)
            {
                _discoTime.Start();
                return;
            }
		    
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;

            lock (_discoLock)
            {
                _working = true;

                try
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (_pendingCommand.Count > 0)
                        {
                            DiscoItem discoItem = (DiscoItem)_pendingCommand[0];
                            _pendingCommand.RemoveAt(0);

                            _discoManager.DisoverItems(discoItem.Jid,
                                                        Uri.COMMANDS,
                                                        new IqCB(OnCommandsServerResult),
                                                        new DiscoverySessionData(discoItem));

                        }
                        else if (_pendingDiscoInfo.Count > 0)
                        {
                            DiscoItem discoItem = (DiscoItem) _pendingDiscoInfo[0];
                            _pendingDiscoInfo.RemoveAt(0);

                            ServicesCount++;

                            if (string.IsNullOrEmpty(discoItem.Node))
                            {
                                _discoManager.DisoverInformation(discoItem.Jid, new IqCB(OnDiscoInfoResult),
                                                                 new DiscoverySessionData(discoItem));
                            }
                            else
                            {
                                _discoManager.DisoverInformation(discoItem.Jid, discoItem.Node,
                                                                 new IqCB(OnDiscoInfoResult),
                                                                 new DiscoverySessionData(discoItem));
                            }
                            
                            // one option
                            Jid jid;

                            jid = discoItem.Jid;

                            if (discoItem.Node != null)
                            {
                                _discoManager.DisoverItems(jid, discoItem.Node, new IqCB(OnDiscoServerResult),
                                                           new DiscoverySessionData(discoItem));
                            }
                            else
                            {
                                _discoManager.DisoverItems(jid, new IqCB(OnDiscoServerResult),
                                                           new DiscoverySessionData(discoItem));
                            }
                        }
                    }
                }

                finally
                {
                    _working = false;
                    _discoTime.Start();
                }
            }
		}

		public void StopDiscovery()
		{
            _pendingDiscoInfo.Clear();

			Services.Instance.StopSession() ;
		}

		private void _xmppConnection_OnIq( object sender, IQ iq )
		{
		}

		private void _xmppConnection_OnAuthError( object sender, Element e )
		{
			EventError eventError = new EventError( Resources.Event_AuthFailed, null ) ;
			Events.Instance.OnEvent( this, eventError ) ;
		}

		private void _xmppConnection_OnError( object sender, Exception ex )
		{
			EventError eventError = new EventError( ex.Message, null ) ;
			Events.Instance.OnEvent( this, eventError ) ;
		}

		private void _xmppConnection_OnPresence( object sender, Presence pres )
		{
			if ( pres.MucUser != null )
			{
				return ;
			}

			if ( pres.Error != null )
			{
				EventError eventError = new EventError( string.Format( "Presence error from {0}", pres.From ),
				                                        pres.Error ) ;
				Events.Instance.OnEvent( this, eventError ) ;
			}
			else
			{
				switch ( pres.Type )
				{
					case PresenceType.subscribe:
						{
							break ;
						}
					case PresenceType.subscribed:
						{
							break ;
						}
					case PresenceType.unsubscribe:
						{
							break ;
						}
					case PresenceType.unsubscribed:
						{
							break ;
						}
					default:
						{
							Roster.Instance.OnPresence( sender, pres ) ;
							break ;
						}
				}
			}
		}

		private void _xmppConnection_OnRosterEnd( object sender )
		{
			SendMyPresence() ;
		}

        private void DiscoveryInternal( string serverJid )
        {
            StopDiscovery();

            Services.Instance.Clear();

            ServicesDoneCount = 0;
            ServicesCount = 0;

            Jid jid;

            if (string.IsNullOrEmpty(serverJid))
            {
                jid = new Jid(XmppConnection.Server);
            }
            else
            {
                jid = new Jid(serverJid);
            }

            Discovery(jid);           
        }

		public void Discovery( string serverJid )
		{
		    DiscoveryInternal(serverJid);
		}

		private void SendMyPresence()
		{
			XmppConnection.Show = Settings.Default.XmppMyPresence ;
			XmppConnection.SendMyPresence() ;
		}

		private void _xmppConnection_OnRosterItem( object sender, RosterItem item )
		{
			Roster.Instance.OnRosterItem( sender, item ) ;
		}

	    private MucMarkManager _mucMarkManager;

		private void _xmppConnection_OnLogin( object sender )
		{
			IsLogged = true ;

            _mucMarkManager = new MucMarkManager(XmppConnection);
		    _mucMarkManager.LoadMucMarks();
		}

		private void _xmppConnection_OnClose( object sender )
		{
			IsLogged = false ;
		}

		private void Discovery( Jid jid )
		{
			_discoManager.DisoverItems( jid, new IqCB( OnDiscoServerResult ), new DiscoverySessionData( null ) ) ;
		}

        public void AddDiscoInfo(DiscoItem discoItem)
        {
            _pendingDiscoInfo.Add(discoItem);
        }

        public void AddDiscoInfoPrioritized(DiscoItem discoItem)
        {
            _pendingDiscoInfo.Insert(0, discoItem);
        }

        void AddCommand(DiscoItem discoItem)
        {
            _pendingCommand.Add(discoItem);
        }
        
        public int MyPriority
		{
			get
			{
				return XmppConnection.Priority ;
			}
		}

		public Jid MyJid
		{
			get
			{
				return XmppConnection.MyJID ;
			}
		}

		public bool IsLogged
		{
			get
			{
				return _isLogged ;
			}

			protected set
			{
				if ( _isLogged != value )
				{
					NotifyPropertyChanged( "IsLogged" ) ;
				}

				_isLogged = value ;
			}
		}

		public int ServicesCount
		{
			get
			{
				return _servicesCount ;
			}
			set
			{
				_servicesCount = value ;

				NotifyPropertyChanged( "ServicesCount" ) ;
			}
		}

		public int ServicesDoneCount
		{
			get
			{
				return _servicesDoneCount ;
			}
			set
			{
				_servicesDoneCount = value ;

				NotifyPropertyChanged( "ServicesDoneCount" ) ;
			}
		}

	    public MucMarkManager MucMarkManager
	    {
	        get
	        {
	            return _mucMarkManager;
	        }
	    }

	    public XmppClientConnection XmppConnection
	    {
	        get
	        {
	            return _xmppConnection;
	        }
	    }

	    private static bool CheckSessionKey( object data )
		{
			DiscoverySessionData sessionData = data as DiscoverySessionData ;

			return ( Services.Instance.SessionKey != string.Empty
			         && sessionData.SessionKey == Services.Instance.SessionKey ) ;
		}

		private void OnDiscoServerResult( object sender, IQ iq, object data )
		{
			if ( !CheckSessionKey( data ) )
			{
				return ;
			}

			if ( iq.Error != null )
			{
                EventInfo eventError = new EventInfo(string.Format(Resources.Error_DiscoFailed, iq.From));
				Events.Instance.OnEvent( this, eventError ) ;
			}
			else if ( iq.Type == IqType.result )
			{
				Element query = iq.Query ;

				if ( query != null && query is DiscoItems )
				{
					DiscoItems items = query as DiscoItems ;
					DiscoItem[] itms = items.GetDiscoItems() ;

                    DiscoverySessionData sessionData = data as DiscoverySessionData;
                    Services.Instance.OnServiceItem(sender, itms, sessionData.Data as DiscoItem);
				}
			}
		}

    	private void OnDiscoInfoResult( object sender, IQ iq, object data )
		{
			if ( !CheckSessionKey( data ) )
			{
				return ;
			}

			if ( iq.Error != null )
			{
				ServicesDoneCount++ ;

				Services.Instance.OnServiceItemError( sender, iq ) ;
			}
			else if ( iq.Type == IqType.result && iq.Query is DiscoInfo )
			{
				DiscoInfo di = iq.Query as DiscoInfo ;

				DiscoverySessionData sessionData = data as DiscoverySessionData ;

				DiscoItem discoItem = sessionData.Data as DiscoItem ;

				Services.Instance.OnServiceItemInfo( sender, discoItem, di ) ;

				ServicesDoneCount++ ;

				if ( di.HasFeature( Uri.COMMANDS ) && di.Node == null )
				{
				    AddCommand(discoItem);
				}
			}
		}

		private void OnCommandsServerResult( object sender, IQ iq, object data )
		{
			if ( !CheckSessionKey( data ) )
			{
				return ;
			}

			if ( iq.Error != null )
			{
				EventError eventError = new EventError( string.Format( Resources.Error_CommandResultFailed, iq.From ), iq.Error ) ;
				Events.Instance.OnEvent( this, eventError ) ;
			}
			else if ( iq.Type == IqType.result )
			{
				DiscoverySessionData discoverySessionData = data as DiscoverySessionData ;
				Services.Instance.OnCommandsItemInfo( this, discoverySessionData.Data as DiscoItem, iq ) ;
			}
		}

		public void DoSearchService( Service service, Data data )
		{
			SearchIq searchIq = new SearchIq( IqType.set, service.Jid ) ;
			searchIq.To = service.Jid ;
			searchIq.Query.AddChild( data ) ;

			XmppConnection.IqGrabber.SendIq( searchIq, OnServiceSearched, service ) ;
		}

		public void DoSearchService( Service service, string first, string last, string nick, string email )
		{
			SearchIq searchIq = new SearchIq( IqType.set, service.Jid ) ;
			searchIq.Query.Firstname = first ;
			searchIq.Query.Lastname = last ;
			searchIq.Query.Nickname = nick ;
			searchIq.Query.Email = email ;

			XmppConnection.IqGrabber.SendIq( searchIq, OnServiceSearched, service ) ;
		}

		private void OnServiceSearched( object sender, IQ iq, object data )
		{
			Service service = data as Service ;

			agsXMPP.protocol.iq.search.Search search = iq.Query as agsXMPP.protocol.iq.search.Search ;

			if ( iq.Error != null )
			{
				EventError eventError = new EventError( string.Format( Resources.Event_SearchFailed,
				                                                       service.Name, iq.Error.Condition ), iq.Error ) ;
				Events.Instance.OnEvent( this, eventError ) ;
			}
			else if ( iq.Type == IqType.result && search != null )
			{
				Search.Instance.DisplaySearchResult( search, ( Service ) data ) ;

				EventInfo eventinfo = new EventInfo( string.Format( Resources.Even_SearchSucceeded, service.Name ) ) ;
				Events.Instance.OnEvent( this, eventinfo ) ;
			}
		}

		public void DoRegisterService( Service service, Data data )
		{
			RegisterIq registerIq = new RegisterIq( IqType.set, service.Jid ) ;
			registerIq.To = service.Jid ;
			registerIq.Query.AddChild( data ) ;

			XmppConnection.IqGrabber.SendIq( registerIq, OnServiceRegistered, service ) ;
		}

		public void DoRegisterService( Service service, Dictionary< string, string > values )
		{
			RegisterIq registerIq = new RegisterIq( IqType.set, service.Jid ) ;

			foreach ( KeyValuePair< string, string > value in values )
			{
				registerIq.Query.AddTag( value.Key, value.Value ) ;
			}

			XmppConnection.IqGrabber.SendIq( registerIq, OnServiceRegistered, service ) ;
		}

		private void OnServiceRegistered( object sender, IQ iq, object data )
		{
			Service service = data as Service ;

			if ( iq.Error != null )
			{
				EventError eventError = new EventError( string.Format( Resources.Event_RegistrationFailed,
				                                                       service.Name, iq.Error.Condition ), iq.Error ) ;
				Events.Instance.OnEvent( this, eventError ) ;
			}
			else if ( iq.Type == IqType.result )
			{
				EventInfo eventinfo = new EventInfo( string.Format( Resources.Event_RegistrationSucceeded, service.Name ) ) ;
				Events.Instance.OnEvent( this, eventinfo ) ;
			}
		}

		public void GetService( Service service )
		{
			RegisterIq registerIq = new RegisterIq( IqType.get, service.Jid ) ;

			XmppConnection.IqGrabber.SendIq( registerIq, OnRegisterServiceGet, service ) ;
		}

		public void GetServiceSearch( Service service )
		{
			SearchIq searchIq = new SearchIq( IqType.get, service.Jid ) ;

			XmppConnection.IqGrabber.SendIq( searchIq, OnRegisterServiceGetSearch, service ) ;
		}

		private void OnRegisterServiceGetSearch( object sender, IQ iq, object data )
		{
			agsXMPP.protocol.iq.search.Search search = iq.Query as agsXMPP.protocol.iq.search.Search ;

			if ( iq.Error != null )
			{
				Service service = data as Service ;

				EventError eventError = new EventError( string.Format( Resources.Event_SearchInfoFailed,
				                                                       service.Name ), iq.Error ) ;

				Events.Instance.OnEvent( this, eventError ) ;
			}
			else if ( iq.Type == IqType.result && search != null )
			{
				Search.Instance.DisplaySearch( search, ( Service ) data ) ;
			}
		}

		private void OnRegisterServiceGet( object sender, IQ iq, object data )
		{
			Register register = iq.Query as Register ;

			if ( iq.Error != null )
			{
				Service service = data as Service ;

				EventError eventError = new EventError( string.Format( Resources.Event_RegInfoFailed,
				                                                       service.Name ), iq.Error ) ;

				Events.Instance.OnEvent( this, eventError ) ;
			}
			else if ( iq.Type == IqType.result && register != null )
			{
				Registration.Instance.DisplayInBandRegistration( register, ( Service ) data ) ;
			}
		}

		public void ServiceCommand( Service service )
		{
			IQ commandIq = new IQ( IqType.set ) ;

			commandIq.GenerateId() ;
			commandIq.To = service.Jid ;

			Command command = new Command( service.Node ) ;
			command.Action = Action.execute ;

			commandIq.AddChild( command ) ;

			XmppConnection.IqGrabber.SendIq( commandIq, OnCommandResult, service ) ;
		}

		private void OnCommandResult( object sender, IQ iq, object data )
		{
			if ( iq.Error != null )
			{
				Service service = data as Service ;

				EventError eventError = new EventError( string.Format( Resources.Event_CommandExecFailed,
				                                                       service.Name ), iq.Error ) ;

				Events.Instance.OnEvent( this, eventError ) ;
			}
			else if ( iq.Type == IqType.result )
			{
				foreach ( Node node in iq.ChildNodes )
				{
					Command command = node as Command ;

					if ( command != null )
					{
						Service service = data as Service ;

						if ( service == null )
						{
							service = ( data as ServiceCommandExecution ).Service ;
						}

						CommandExecutor.Instance.DisplayQuestionaire( command, service ) ;
						break ;
					}
				}
			}
		}

		public void Close()
		{
			if ( IsLogged )
			{
				XmppConnection.Close() ;
			}
		}

		protected void ExecuteServiceCommand( ServiceCommandExecution command, Action action )
		{
			IQ commandIq = new IQ( IqType.set ) ;

			commandIq.GenerateId() ;
			commandIq.To = command.Service.Jid ;

			Command commandExec = new Command( command.Command.Node ) ;
			commandExec.Action = action ;
			commandExec.SessionId = command.Command.SessionId ;
			commandExec.Data = command.GetResult() ;

			commandIq.AddChild( commandExec ) ;

			XmppConnection.IqGrabber.SendIq( commandIq, OnCommandResult, command ) ;
		}

		public void ServiceCommandComplete( ServiceCommandExecution command )
		{
			ExecuteServiceCommand( command, Action.complete ) ;
		}

		public void ServiceCommandNext( ServiceCommandExecution command )
		{
			ExecuteServiceCommand( command, Action.next ) ;
		}

		public void ServiceCommandPrevious( ServiceCommandExecution command )
		{
			ExecuteServiceCommand( command, Action.prev ) ;
		}

		public void ServiceCommandCancel( ServiceCommandExecution command )
		{
			ExecuteServiceCommand( command, Action.cancel ) ;
		}

		public void RequestVCard( RosterItem item )
		{
		}

		public void JoinMuc(string jidBare)
		{
            DiscoverSingleService(jidBare);
		}

        public void DiscoverSingleService(string jidBare)
        {
            Jid jid = new Jid(jidBare);

            _discoManager.DisoverInformation(jid, new IqCB(OnSingleDiscoverResult));
        }

        private void OnSingleDiscoverResult(object sender, IQ iq, object data)
        {
            if (iq.Error != null)
            {
                Services.Instance.OnServiceItemError(sender, iq);
            }
            else if (iq.Type == IqType.result && iq.Query is DiscoInfo)
            {
                Service service = new Service(new DiscoItem(), false);
                service.DiscoItem.Jid = iq.From;
                service.DiscoInfo = (DiscoInfo)iq.Query;

                DiscoverReservedRoomNickname(service);
            }
        }

	    public void JoinMuc( Service service )
		{
			DiscoverReservedRoomNickname( service ) ;
		}
		
		protected void DiscoverReservedRoomNickname( Service service )
		{
			IQ iq = new IQ( IqType.get, MyJid, service.Jid ) ;

			iq.GenerateId() ;
			DiscoInfo di = new DiscoInfo() ;
			di.Node = "x-roomuser-item" ;
			iq.Query = di ;

			XmppConnection.IqGrabber.SendIq( iq, new IqCB( OnRoomNicknameResult ), service ) ;
		}

		private void OnRoomNicknameResult( object sender, IQ iq, object data )
		{
			string nick = null ;

			if ( iq.Error != null )
			{
				EventError eventError = new EventError( string.Format( Resources.Error_RoomNickFailed,
				                                                       iq.From ), iq.Error ) ;

				Events.Instance.OnEvent( this, eventError ) ;
			}
			else if ( iq.Type == IqType.result && iq.Query is DiscoInfo )
			{
				DiscoInfo di = iq.Query as DiscoInfo ;

				if ( di != null && di.Node == "x-roomuser-item"
				     && di.GetIdentities() != null
				     && di.GetIdentities().Length > 0 )
				{
					nick = di.GetIdentities()[ 0 ].Name ;
				}
			}

            if (data is Service)
            {
                JoinMuc(data as Service, nick);
            }
            else
            {
                JoinMuc(data as MucMark, nick);
            }
		}

		protected void JoinMuc( Service service, string nick )
		{
			MucInfo.Instance.MucLogin( service, nick ) ;
		}

        protected void JoinMuc(MucMark mucMark, string nick)
        {
            if (!string.IsNullOrEmpty(nick))
            {
                mucMark.Nick = nick;
            }
            _discoManager.DisoverInformation(new Jid(mucMark.Jid), new IqCB(OnDiscoInfoResultMucRoom),
                                             mucMark);
        }

        private void OnDiscoInfoResultMucRoom(object sender, IQ iq, object data)
        {
            if (iq.Error != null)
            {
                Services.Instance.OnServiceItemError(sender, iq);
            }
            else if (iq.Type == IqType.result && iq.Query is DiscoInfo)
            {
                DiscoInfo di = iq.Query as DiscoInfo;

                MucMark mucMark = data as MucMark;
                mucMark.DiscoInfo = di;

                MucInfo.Instance.MucLogin(mucMark);
            }
        }

        public MucRoom JoinMuc(Service service, string nick, string password)
		{
			if ( service.IsMucPasswordProtected )
			{
				_mucManager.JoinRoom( service.Jid, nick, password ) ;
			}
			else
			{
				_mucManager.JoinRoom( service.Jid, nick ) ;
			}

			return new MucRoom( service, XmppConnection, nick ) ;
		}

        public MucManager GetMucManager()
        {
            return new MucManager(XmppConnection);
        }

        public void DoSaveMucConfig(MucRoom mucRoom, Data data)
        {
            OwnerIq saveIq = new OwnerIq(IqType.set, mucRoom.Service.Jid);

            saveIq.Query.AddChild(data);

            XmppConnection.IqGrabber.SendIq(saveIq, OnMucConfigSaved, mucRoom);
        }

        private void OnMucConfigSaved(object sender, IQ iq, object data)
        {
            MucRoom mucRoom = data as MucRoom;

            if (iq.Error != null)
            {
                EventError eventError = new EventError(string.Format(Resources.EventError_MucConfigFailed,
                                                                       mucRoom.Service.Name, iq.Error.Condition), iq.Error);
                Events.Instance.OnEvent(this, eventError);
            }
        }
    }
}