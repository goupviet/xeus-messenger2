﻿using agsXMPP;
using xeus2.xeus.Core;
using xeus2.xeus.UI;

namespace xeus2.xeus.Middle
{
    internal class Authorization
    {
        private static readonly Authorization _instance = new Authorization();

        public static Authorization Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Show(Contact contact)
        {
            try
            {
                AskAuthorization authorization = new AskAuthorization(contact);

                authorization.Show();
            }

            catch (WindowExistsException e)
            {
                e.ExistingWindow.Activate();
            }
        }

        public void Ask(Contact contact)
        {
            App.InvokeSafe(App._dispatcherPriority,
                           new ShowCallback(Show), contact);
        }

        #region Nested type: ShowCallback

        private delegate void ShowCallback(Contact contact);

        #endregion
    }
}