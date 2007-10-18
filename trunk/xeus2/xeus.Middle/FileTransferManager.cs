﻿using System;
using System.Collections.Generic;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using Microsoft.Win32;
using xeus2.xeus.Core;
using xeus2.xeus.UI;

namespace xeus2.xeus.Middle
{
    internal class FileTransferManager
    {
        private static readonly FileTransferManager _instance = new FileTransferManager();

        public static FileTransferManager Instance
        {
            get
            {
                return _instance;
            }
        }

        static IContact FindContact(Jid jid)
        {
            Contact contact = null;

            lock (Roster.Instance.Items._syncObject)
            {
                contact = Roster.Instance.FindContact(jid);
            }

            if (contact == null)
            {
                Presence presence = new Presence(ShowType.NONE, string.Empty);
                presence.From = jid;

                contact = new Contact(presence);
            }

            return contact;
        }

        void TransferOpenUI(XmppClientConnection xmppCon, IQ iq)
        {
            FileTransfer fileTransfer = new FileTransfer(xmppCon, iq, FindContact(iq.From));
            FileTransfer.FileTransfers.Add(fileTransfer);

            try
            {
                FileTransferWindow fileTransferWindow = new FileTransferWindow();

                fileTransferWindow.Show();
            }

            catch (WindowExistsException e)
            {
                e.ActivateControl();
            }
        }

        void TransferOpenUI(XmppClientConnection xmppCon, IContact contact, string filename)
        {
            FileTransfer fileTransfer = new FileTransfer(xmppCon, contact, filename);
            FileTransfer.FileTransfers.Add(fileTransfer);

            try
            {
                FileTransferWindow fileTransferWindow = new FileTransferWindow();

                fileTransferWindow.Show();
            }

            catch (WindowExistsException e)
            {
                e.ActivateControl();
            }
        }

        public void TransferOpen(XmppClientConnection xmppCon, IQ iq)
        {
            App.InvokeSafe(App._dispatcherPriority,
                           new DisplayCallback(TransferOpenUI), xmppCon, iq);
        }

        #region Nested type: DisplayCallback

        private delegate void DisplayCallback(XmppClientConnection xmppCon, IQ iq);

        #endregion

        private delegate void SendCallback(IContact contact);

        public void SendFile(IContact contact)
        {
            App.InvokeSafe(App._dispatcherPriority,
                           new SendCallback(SendFileInternal), contact);
        }

        void SendFileInternal(IContact contact)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            // dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".*"; // Default file extension
            dlg.Filter = "Word Processor Files (.*)|*.*"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                TransferOpenUI(Account.Instance.XmppConnection, contact, dlg.FileName);
            }            
        }
    }
}