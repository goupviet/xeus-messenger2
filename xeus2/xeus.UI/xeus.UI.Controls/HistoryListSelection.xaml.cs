﻿using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using xeus2.xeus.Commands;
using xeus2.xeus.Core;
using xeus2.xeus.Middle;

namespace xeus2.xeus.UI.xeus.UI.Controls
{
    /// <summary>
    /// Interaction logic for HistoryListSelection.xaml
    /// </summary>
    public partial class HistoryListSelection : UserControl
    {
        public HistoryListSelection()
        {
            InitializeComponent();

            _list.SelectedItem = null;
            _list.SelectionChanged += _list_SelectionChanged;
        }

        void _list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;

            if (_list.SelectedItem != null)
            {
                Recent recent = (Recent)_list.SelectedItem;

                if (recent.Item != null)
                {
                    if (recent.Item is IContact)
                    {
                        if (AccountCommands.DisplayChat.CanExecute((IContact)recent.Item, null))
                        {
                            AccountCommands.DisplayChat.Execute((IContact)recent.Item, null);
                        }
                    }
                    else
                    {
                        MucInfo.Instance.MucLogin((Service) recent.Item, null);
                    }
                }

                _list.SelectedItem = null;

                CloseParentPopup();
            }
        }

        private void CloseParentPopup()
        {
            Popup popup = Parent as Popup;

            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }
    }
}