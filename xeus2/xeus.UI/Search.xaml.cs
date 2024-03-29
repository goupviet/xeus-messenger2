using System.Windows ;
using xeus2.xeus.Core ;

namespace xeus2.xeus.UI
{
	/// <summary>
	/// Interaction logic for Search.xaml
	/// </summary>
	public partial class Search : BaseWindow
	{
	    public const string _keyBase = "Search";

		internal Search( agsXMPP.protocol.iq.search.Search search, Service service ) : base(_keyBase, service.Jid.Bare)
		{
			InitializeComponent() ;

			_search.Setup( search, service ) ;
		}

		internal void DisplaySearchResult( agsXMPP.protocol.iq.search.Search search, Service service )
		{
			_resultView.ReadSearchResult( search ) ;
		}

		protected void OnSearch( object sender, RoutedEventArgs eventArgs )
		{
			if ( !_search.IsValid )
			{
				return ;
			}

			if ( _search.XData != null )
			{
				Account.Instance.DoSearchService( _search.Service, _search.GetResult() ) ;
			}
			else
			{
				_search.UpdateData() ;

				Account.Instance.DoSearchService( _search.Service, _search.FirstName,
				                                  _search.LastName, _search.Nickname,
				                                  _search.Email ) ;
			}
		}
	}
}