using System.Windows.Input;
using xeus2.xeus.Core;
using xeus2.xeus.Middle;

namespace xeus2.xeus.Commands
{
    public static class AccountCommands
    {
        private static readonly RoutedUICommand _displayHeadlines =
            new RoutedUICommand("Display Headlines", "DisplayHeadlines", typeof (AccountCommands));

        private static readonly RoutedUICommand _displayLogin =
            new RoutedUICommand("Display Login", "DisplayLogin", typeof (AccountCommands));

        private static readonly RoutedUICommand _displayMucMarks =
            new RoutedUICommand("Display MUC Bookmarks", "DisplayMUCBookmarks", typeof (AccountCommands));

        private static readonly RoutedUICommand _displayWizard =
            new RoutedUICommand("Display Wizard", "DisplayWizard", typeof (AccountCommands));

        private static readonly RoutedUICommand _history =
            new RoutedUICommand("Display History", "DisplayHistory", typeof (AccountCommands));

        private static readonly RoutedUICommand _login =
            new RoutedUICommand("Login", "login", typeof(AccountCommands));

        public static RoutedUICommand DisplayLogin
        {
            get
            {
                return _displayLogin;
            }
        }

        public static RoutedUICommand History
        {
            get
            {
                return _history;
            }
        }

        public static RoutedUICommand DisplayMucMarks
        {
            get
            {
                return _displayMucMarks;
            }
        }

        public static RoutedUICommand DisplayHeadlines
        {
            get
            {
                return _displayHeadlines;
            }
        }

        public static RoutedUICommand DisplayWizard
        {
            get
            {
                return _displayWizard;
            }
        }

        public static RoutedUICommand Login
        {
            get
            {
                return _login;
            }
        }

        public static void RegisterCommands(CommandBindingCollection commandBindings)
        {
            commandBindings.Add(
                new CommandBinding(_displayLogin, ExecuteDisplayLogin, CanExecuteDisplayLogin));

            commandBindings.Add(
                new CommandBinding(_displayWizard, ExecuteDisplayWizard, CanExecuteDisplayWizard));

            commandBindings.Add(
                new CommandBinding(_displayHeadlines, ExecuteDisplayHeadlines, CanExecuteDisplayHeadlines));

            commandBindings.Add(
                new CommandBinding(_history, ExecuteDisplayHistory, CanExecuteDisplayHistory));

            commandBindings.Add(
                new CommandBinding(_displayMucMarks, ExecuteDisplayMucMarks, CanExecuteDisplayMucMarks));

            commandBindings.Add(
                new CommandBinding(_login, ExecuteLogin, CanExecuteLogin));
        }

        private static void CanExecuteLogin(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private static void ExecuteLogin(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;

            Account.Instance.Login();
        }

        private static void CanExecuteDisplayWizard(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private static void ExecuteDisplayWizard(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            Wizard.Instance.DisplayWizard();
        }

        private static void CanExecuteDisplayHeadlines(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private static void ExecuteDisplayHeadlines(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            Headlines.Instance.DisplayHeadlines();
        }

        private static void CanExecuteDisplayMucMarks(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (MucMarks.Instance.Count > 0);
            e.Handled = true;
        }

        private static void ExecuteDisplayMucMarks(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = false;
        }

        private static void CanExecuteDisplayHistory(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (RecentItems.Instance.Count > 0);
            e.Handled = true;
        }

        private static void ExecuteDisplayHistory(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = false;
        }

        public static void CanExecuteDisplayLogin(object sender, CanExecuteRoutedEventArgs e)
        {
            e.Handled = true;
            e.CanExecute = true;
        }

        public static void ExecuteDisplayLogin(object sender, ExecutedRoutedEventArgs e)
        {
            Middle.Login.Instance.DisplayLogin();

            e.Handled = true;
        }
    }
}