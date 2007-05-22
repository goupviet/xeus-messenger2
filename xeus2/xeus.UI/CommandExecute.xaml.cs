using System.Windows ;
using xeus2.xeus.Commands ;
using xeus2.xeus.Core ;

namespace xeus2.xeus.UI
{
	/// <summary>
	/// Interaction logic for CommandExecute.xaml
	/// </summary>
	public partial class CommandExecute : Window
	{
		internal CommandExecute( ServiceCommandExecution serviceCommandExecution )
		{
			InitializeComponent() ;

			_execute.Setup( serviceCommandExecution ) ;
		}

		protected override void OnInitialized( System.EventArgs e )
		{
			base.OnInitialized( e );

			ServiceCommands.RegisterCommands( this );
		}
	}
}