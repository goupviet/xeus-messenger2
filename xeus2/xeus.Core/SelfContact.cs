﻿using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.vcard;
using xeus.Data;
using xeus2.Properties;

namespace xeus2.xeus.Core
{
    public class SelfContact : NotifyInfoDispatcher, IContact
    {
        private BitmapImage _image;
        private string _fullName;
        private string _nickName;

        #region IContact Members

        public Jid Jid
        {
            get
            {
                return Account.Instance.XmppConnection.MyJID;
            }
        }

        public Presence Presence
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Priority
        {
            get
            {
                return Account.Instance.XmppConnection.Priority;
            }

            set
            {
                Settings.Default.XmppPriority = value;
                Account.Instance.SendMyPresence();
            }
        }

        public string Resource
        {
            get
            {
                return Account.Instance.XmppConnection.Resource;
            }

            set
            {
                Settings.Default.XmppResource = value;
                Account.Instance.SendMyPresence();
            }
        }

        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(CustomName))
                {
                    return CustomName;
                }
                else if (!string.IsNullOrEmpty(FullName))
                {
                    return FullName;
                }
                else if (!string.IsNullOrEmpty(NickName))
                {
                    return NickName;
                }

                return Jid.ToString();
            }
        }

        public string Group
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAvailable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Show
        {
            get
            {
                return Account.Instance.XmppConnection.Show.ToString();
            }
        }

        public ShowType MyShow
        {
            get
            {
                return Account.Instance.XmppConnection.Show;
            }

            set
            {
                Settings.Default.XmppMyPresence = value;
                Account.Instance.SendMyPresence();
            }
        }

        public string StatusText
        {
            get
            {
                return Account.Instance.XmppConnection.Status;
            }

            set
            {
                Settings.Default.XmppStatusText = value;
                Account.Instance.SendMyPresence();
            }
        }

        public string XStatusText
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string FullName
        {
            get
            {
                return _fullName;
            }
        }

        public string NickName
        {
            get
            {
                return _nickName;
            }
        }

        public BitmapImage Image
        {
            get
            {
                return _image;
            }
        }

        public bool IsImageTransparent
        {
            get
            {
                if (_image == null)
                {
                    return true;
                }
                else
                {
                    return (_image.Format.Masks.Count >= 4);
                }
            }
        }

        public string CustomName
        {
            get
            {
                return null;
            }
        }

        public bool IsService
        {
            get
            {
                return false;
            }
        }

        public string ClientVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string ClientNode
        {
            get
            {
                return "http://xeus.net/caps";
            }
        }

        public string[] ClientExtensions
        {
            get
            {
                return null;
            }
        }

        public void PresenceChange()
        {
            NotifyPropertyChanged("Show");
            NotifyPropertyChanged("StatusText");
            NotifyPropertyChanged("MyShow");
            NotifyPropertyChanged("Priority");
            NotifyPropertyChanged("Resource");
        }

        #endregion

        public void AskMyVcard()
        {
            VcardIq viq = new VcardIq(IqType.get, Jid);
            Account.Instance.XmppConnection.IqGrabber.SendIq(viq, new IqCB(VcardResult), null);
        }

        private void VcardResult(object sender, IQ iq, object data)
        {
            if (iq.Type == IqType.error || iq.Error != null)
            {
                if (iq.Error.Code == ErrorCode.NotFound)
                {
                    SetMyVcard(null);
                }
                else
                {
                    Events.Instance.OnEvent(this,
                                            new EventError("Error receiving my V-Card", null));
                }
            }
            else if (iq.Type == IqType.result)
            {
                SetMyVcard(iq.Vcard);

                //save it
                if (iq.Vcard != null)
                {
                    Storage.CacheVCard(iq.Vcard, Jid.Bare);
                }
            }
        }

        private void SetMyVcard(Vcard vcard)
        {
            if (vcard != null)
            {
                string hash;
                _image = Storage.ImageFromPhoto(vcard.Photo, out hash);

                _fullName = vcard.Fullname;
                _nickName = vcard.Nickname;
            
                NotifyPropertyChanged("FullName");
                NotifyPropertyChanged("NickName");
                NotifyPropertyChanged("DisplayName");
            }

            if (_image == null)
            {
                _image = Storage.GetDefaultAvatar();
            }

            NotifyPropertyChanged("Image");
            NotifyPropertyChanged("IsImageTransparent");
        }

        public void LoadMyAvatar()
        {
            NotifyPropertyChanged("Resource");
            NotifyPropertyChanged("Priority");
            NotifyPropertyChanged("Jid");
 
            Vcard vcard = Storage.GetVcard(Jid, 9999);
            SetMyVcard(vcard);
        }
    }
}