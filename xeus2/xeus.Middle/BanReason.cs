using xeus2.xeus.Core;

namespace xeus2.xeus.Middle
{
    internal class BanReason
    {
        private static readonly BanReason _instance = new BanReason();

        public static BanReason Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Ban(MucContact mucContact)
        {
            UI.SingleValueBox banReason = new UI.SingleValueBox("Ban reason", "Ban user");
            banReason.DataContext = mucContact;

            banReason.Activate();
            if ((bool) banReason.ShowDialog())
            {
                mucContact.MucRoom.Ban(mucContact, banReason.Text);
            }
        }
    }
}