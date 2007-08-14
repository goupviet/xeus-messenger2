using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using xeus.Data;
using xeus2.xeus.Commands;

namespace xeus2.xeus.UI
{
    /// <summary>
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public ServiceWindow()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            base.EndInit();

            new TextFilterService(_servicesResult.ItemsSource as ListCollectionView, _filter);

            new TextFilterMucMark(_mucMarksResult.ItemsSource as ListCollectionView, _filterMucMarks);

            new TextFilterMucRoom(_mucResult.ItemsSource as ListCollectionView, _filterMuc, _checkDispEmpty);

            ServiceCommands.RegisterCommands(this);
            MucCommands.RegisterCommands(this);
        }

        private void MucMarkDblClick(object sender, MouseEventArgs args)
        {
            ListBox list = sender as ListBox;
            
            if (list != null
                && list.SelectedItems.Count > 0
                && ServiceCommands.JoinMuc.CanExecute(list.SelectedItems[0], list))
            {
                ServiceCommands.JoinMuc.Execute(list.SelectedItems[0], list);
            }
        } 

        private void MucDblClick(object sender, MouseEventArgs args)
        {
            ListView list = sender as ListView;
           
            if (list != null
                && list.SelectedItems.Count > 0
                && ServiceCommands.JoinMuc.CanExecute(list.SelectedItems[0], list))
            {
                ServiceCommands.JoinMuc.Execute(list.SelectedItems[0], list);
            }
        } 

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Database.SaveMucMarks();
        }
    }
}