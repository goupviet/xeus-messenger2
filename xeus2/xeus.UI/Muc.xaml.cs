using xeus2.xeus.Commands;
using xeus2.xeus.Core;
using xeus2.xeus.UI.xeus.UI.Controls;

namespace xeus2.xeus.UI
{
    /// <summary>
    /// Interaction logic for Muc.xaml
    /// </summary>
    public partial class Muc : BaseWindow
    {
        public const string _keyBase = "MUCRoom";

        internal Muc() : base(_keyBase, string.Empty)
        {
            InitializeComponent();

            _multi.MultiWinContainerProvider = Middle.Muc.Instance;
        }

        internal void AddMuc(Service service, string nick, string password)
        {
            MucConversation conversation = new MucConversation();
            conversation.MucConversationInit(service, nick, password);

            MultiWin multiWin = new MultiWin(conversation, _keyBase, service.Jid.ToString());

            _multi.MultiWindows.Add(new MultiTabItem(service.Name, multiWin));
        }
    }
}