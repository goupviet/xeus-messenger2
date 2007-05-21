using System ;
using agsXMPP ;
using agsXMPP.protocol.client ;
using agsXMPP.protocol.iq.disco ;
using xeus2.Properties ;
using Uri=agsXMPP.Uri;

namespace xeus2.xeus.Core
{
	internal class Service : NotifyInfoDispatcher
	{
		private DiscoInfo _discoInfo = null ;
		private DiscoItem _discoItem = null ;
		private bool _isDiscovered = false ;
		private IQ _errorIq = null ;

		private bool _isCommand = false ;

		private Services _services = new Services() ;

		public Service( DiscoItem discoItem )
		{
			_discoItem = discoItem ;
		}

		public Service( DiscoItem discoItem, bool isCommand ) : this( discoItem )
		{
			_isCommand = isCommand ;
		}

		public Service( DiscoInfo discoInfo )
		{
			DiscoInfo = discoInfo ;
		}

		public Jid Jid
		{
			get
			{
				return DiscoItem.Jid ;
			}
		}

		public string Name
		{
			get
			{
				if ( DiscoInfo == null )
				{
					if ( ErrorIq == null )
					{
						return DiscoItem.Jid.ToString() ;
					}
					else
					{
						return string.Format( Resources.Error_CodeMsg, ErrorIq.Error.Code, ErrorIq.Error.LastNode.Value ) ;
					}
				}

				DiscoIdentity[] discoIdentities = DiscoInfo.GetIdentities() ;

				if ( !String.IsNullOrEmpty( DiscoItem.Name ) )
				{
					return DiscoItem.Name ;
				}
				else if ( discoIdentities.Length > 0 )
				{
					return discoIdentities[ 0 ].Name ;
				}
				else if ( !String.IsNullOrEmpty( Node ) )
				{
					return Node ;
				}

				return Resources.Constant_UnknownService ;
			}
		}

		public string Group
		{
			get
			{
				if ( DiscoInfo != null )
				{
					DiscoIdentity[] discoIdentities = DiscoInfo.GetIdentities() ;

					if ( discoIdentities.Length > 0 )
					{
						return discoIdentities[ 0 ].Category ;
					}
				}

				return Resources.Constant_General ;
			}
		}

		public Services Services
		{
			get
			{
				return _services ;
			}
		}

		public DiscoInfo DiscoInfo
		{
			get
			{
				return _discoInfo ;
			}
			set
			{
				_discoInfo = value ;

				NotifyPropertyChanged( "Name" ) ;
				NotifyPropertyChanged( "Group" ) ;
				NotifyPropertyChanged( "IsRegistrable" ) ;
				NotifyPropertyChanged( "IsSearchable" ) ;
			}
		}

		public bool IsDiscovered
		{
			get
			{
				return _isDiscovered ;
			}

			set
			{
				bool notify = ( _isDiscovered != value ) ;

				_isDiscovered = value ;

				if ( notify )
				{
					NotifyPropertyChanged( "IsDiscovered" ) ;
				}
			}
		}

		public IQ ErrorIq
		{
			get
			{
				return _errorIq ;
			}

			set
			{
				_errorIq = value ;
			}
		}

		public string Node
		{
			get
			{
				return DiscoItem.Node ;
			}
		}

		public DiscoItem DiscoItem
		{
			get
			{
				return _discoItem ;
			}
		}

		public bool IsCommand
		{
			get
			{
				return _isCommand ;
			}
		}

		public bool IsRegistrable
		{
			get
			{
				if ( _discoInfo == null )
				{
					return false ;
				}

				return _discoInfo.HasFeature( Uri.IQ_REGISTER ) ;
			}
		}

		public bool IsSearchable
		{
			get
			{
				if ( _discoInfo == null )
				{
					return false ;
				}

				return _discoInfo.HasFeature( Uri.IQ_SEARCH ) ;
			}
		}

		public override string ToString()
		{
			return Name ;
		}
	}
}