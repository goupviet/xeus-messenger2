using xeus2.xeus.Core;

namespace xeus2.xeus.Middle
{
    internal class KickReason
    {
        private static KickReason _instance = new KickReason();

        public static KickReason Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Kick(MucContact mucContact)
        {
            UI.KickReason kickReason = new UI.KickReason();
            kickReason.DataContext = mucContact;

            if ((bool) kickReason.ShowDialog())
            {
                mucContact.MucRoom.Kick(mucContact, kickReason.Reason);
            }
        }
    }
}