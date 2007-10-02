using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Forms;
using xeus2.xeus.Middle;
using MouseEventArgs=System.Windows.Input.MouseEventArgs;

namespace xeus2.xeus.UI
{
    /// <summary>
    /// Interaction logic for InfoPopup.xaml
    /// </summary>
    public partial class InfoPopup
    {
        public InfoPopup()
        {
            InitializeComponent();

            _info.SizeChanged += _info_SizeChanged;

            Notification.Notifications.CollectionChanged += Notifications_CollectionChanged;
        }

        private void Notifications_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsOpen = (Notification.Notifications.Count > 0);
        }

        private void _info_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Resize();
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            Resize();
        }

        private void Resize()
        {
            Screen[] screens = Screen.AllScreens;

            Screen primaryScreen = screens[0];

            foreach (Screen screen in screens)
            {
                if (screen.Primary)
                {
                    primaryScreen = screen;
                    break;
                }
            }

            HorizontalOffset = primaryScreen.WorkingArea.Right - 10.0;
            VerticalOffset = primaryScreen.WorkingArea.Bottom - 10.0;
        }

        private void Popup_MouseEnter(object sender, MouseEventArgs e)
        {
            Notification.DontExpire = true;
        }

        private void Popup_MouseLeave(object sender, MouseEventArgs e)
        {
            Notification.DontExpire = false;
        }
    }
}