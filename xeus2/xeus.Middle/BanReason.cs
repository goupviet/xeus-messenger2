using xeus2.xeus.Core;

namespace xeus2.xeus.Middle
{
    internal class BanReason
    {
        private static BanReason _instance = new BanReason();

        public static BanReason Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Ban(MucContact mucContact)
        {
            UI.BanReason banReason = new UI.BanReason();
            banReason.DataContext = mucContact;

            if ((bool) banReason.ShowDialog())
            {
                mucContact.MucRoom.Ban(mucContact.UserJid, banReason.Reason);
            }
        }
    }
}