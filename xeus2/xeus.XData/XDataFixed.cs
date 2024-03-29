using System;
using System.Text ;
using System.Windows.Controls ;
using agsXMPP.protocol.x.data ;
using xeus2.xeus.UI ;

namespace xeus2.xeus.XData
{
	internal class XDataFixed : XDataControl
	{
		private TextBlock _textBlock = new TextBlock() ;

		protected override void OnInitialized( System.EventArgs e )
		{
			base.OnInitialized( e );

			_textBlock.Style = StyleManager.GetStyle( "XDataFixed" ) ;
		}

		protected override void OnFieldIsSet()
		{
			base.OnFieldIsSet() ;

            if ( String.IsNullOrEmpty( Field.Description ) )
            {
                _grid.Visibility = System.Windows.Visibility.Collapsed;
            }

			_container.Children.Add( _textBlock ) ;

			StringBuilder stringBuilder = new StringBuilder() ;

			foreach ( string text in Field.GetValues() )
			{
				stringBuilder.Append( text ) ;
			}

			_textBlock.Text = stringBuilder.ToString() ;
		}

		public override Field GetResult()
		{
			Field field = new Field( Field.Var, null, Field.Type );
			field.SetValue( Field.GetValue() );
			
			return field ;
		}

		public override bool Validate()
		{
			return true ;
		}
	}
}