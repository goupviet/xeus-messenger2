﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xeus2.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("xeus")]
        public string XmppResource {
            get {
                return ((string)(this["XmppResource"]));
            }
            set {
                this["XmppResource"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int XmppPriority {
            get {
                return ((int)(this["XmppPriority"]));
            }
            set {
                this["XmppPriority"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("jabber.cz")]
        public string XmppServer {
            get {
                return ((string)(this["XmppServer"]));
            }
            set {
                this["XmppServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("NONE")]
        public global::agsXMPP.protocol.client.ShowType XmppMyPresence {
            get {
                return ((global::agsXMPP.protocol.client.ShowType)(this["XmppMyPresence"]));
            }
            set {
                this["XmppMyPresence"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\"{FullName} ({NickName}) In Group \'{Group}\'\"")]
        public string UI_DisplayFormat {
            get {
                return ((string)(this["UI_DisplayFormat"]));
            }
            set {
                this["UI_DisplayFormat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Goodbye")]
        public string MucLeaveMsg {
            get {
                return ((string)(this["MucLeaveMsg"]));
            }
            set {
                this["MucLeaveMsg"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string XmppUserName {
            get {
                return ((string)(this["XmppUserName"]));
            }
            set {
                this["XmppUserName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string XmppPassword {
            get {
                return ((string)(this["XmppPassword"]));
            }
            set {
                this["XmppPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500")]
        public int UI_ErrorDismiss {
            get {
                return ((int)(this["UI_ErrorDismiss"]));
            }
            set {
                this["UI_ErrorDismiss"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public double UI_GroupMessagesByMinutes {
            get {
                return ((double)(this["UI_GroupMessagesByMinutes"]));
            }
            set {
                this["UI_GroupMessagesByMinutes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UI_DisplayTime_Muc {
            get {
                return ((bool)(this["UI_DisplayTime_Muc"]));
            }
            set {
                this["UI_DisplayTime_Muc"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Small")]
        public global::xeus2.xeus.UI.xeus.UI.Controls.RosterItemSize UI_RosterItemSize {
            get {
                return ((global::xeus2.xeus.UI.xeus.UI.Controls.RosterItemSize)(this["UI_RosterItemSize"]));
            }
            set {
                this["UI_RosterItemSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int VCardExpirationDays {
            get {
                return ((int)(this["VCardExpirationDays"]));
            }
            set {
                this["VCardExpirationDays"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("No status message")]
        public string XmppStatusText {
            get {
                return ((string)(this["XmppStatusText"]));
            }
            set {
                this["XmppStatusText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int UI_MsgOldnest_Recent_Min {
            get {
                return ((int)(this["UI_MsgOldnest_Recent_Min"]));
            }
            set {
                this["UI_MsgOldnest_Recent_Min"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int UI_MsgOldnest_Older_Min {
            get {
                return ((int)(this["UI_MsgOldnest_Older_Min"]));
            }
            set {
                this["UI_MsgOldnest_Older_Min"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public int UI_MsgOldnest_Old_Min {
            get {
                return ((int)(this["UI_MsgOldnest_Old_Min"]));
            }
            set {
                this["UI_MsgOldnest_Old_Min"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("80")]
        public uint UI_MaxHistoryMessages {
            get {
                return ((uint)(this["UI_MaxHistoryMessages"]));
            }
            set {
                this["UI_MaxHistoryMessages"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public uint UI_MaxRecentItems {
            get {
                return ((uint)(this["UI_MaxRecentItems"]));
            }
            set {
                this["UI_MaxRecentItems"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UI_DisplayOfflineContacts {
            get {
                return ((bool)(this["UI_DisplayOfflineContacts"]));
            }
            set {
                this["UI_DisplayOfflineContacts"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("32")]
        public uint UI_MaxAvatarKb {
            get {
                return ((uint)(this["UI_MaxAvatarKb"]));
            }
            set {
                this["UI_MaxAvatarKb"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UI_Notify_PresenceAvailable {
            get {
                return ((bool)(this["UI_Notify_PresenceAvailable"]));
            }
            set {
                this["UI_Notify_PresenceAvailable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UI_Notify_PresenceUnavailable {
            get {
                return ((bool)(this["UI_Notify_PresenceUnavailable"]));
            }
            set {
                this["UI_Notify_PresenceUnavailable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UI_Notify_PresenceOther {
            get {
                return ((bool)(this["UI_Notify_PresenceOther"]));
            }
            set {
                this["UI_Notify_PresenceOther"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public uint UI_Notify_Presence_Exp {
            get {
                return ((uint)(this["UI_Notify_Presence_Exp"]));
            }
            set {
                this["UI_Notify_Presence_Exp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public uint UI_IdleAwayMinutes {
            get {
                return ((uint)(this["UI_IdleAwayMinutes"]));
            }
            set {
                this["UI_IdleAwayMinutes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("8")]
        public uint UI_IdleXAMinutes {
            get {
                return ((uint)(this["UI_IdleXAMinutes"]));
            }
            set {
                this["UI_IdleXAMinutes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1.5")]
        public double UI_WindowZoom {
            get {
                return ((double)(this["UI_WindowZoom"]));
            }
            set {
                this["UI_WindowZoom"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public uint XmppServerPort {
            get {
                return ((uint)(this["XmppServerPort"]));
            }
            set {
                this["XmppServerPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string XmppConnectServer {
            get {
                return ((string)(this["XmppConnectServer"]));
            }
            set {
                this["XmppConnectServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool XmppAutoResolve {
            get {
                return ((bool)(this["XmppAutoResolve"]));
            }
            set {
                this["XmppAutoResolve"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool XmppTLS {
            get {
                return ((bool)(this["XmppTLS"]));
            }
            set {
                this["XmppTLS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool XmppSSL {
            get {
                return ((bool)(this["XmppSSL"]));
            }
            set {
                this["XmppSSL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool XmppCompression {
            get {
                return ((bool)(this["XmppCompression"]));
            }
            set {
                this["XmppCompression"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string XmppBytestreamProxy {
            get {
                return ((string)(this["XmppBytestreamProxy"]));
            }
            set {
                this["XmppBytestreamProxy"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public uint XmppBytestreamProxyPort {
            get {
                return ((uint)(this["XmppBytestreamProxyPort"]));
            }
            set {
                this["XmppBytestreamProxyPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public uint UI_Notify_Error_Exp {
            get {
                return ((uint)(this["UI_Notify_Error_Exp"]));
            }
            set {
                this["UI_Notify_Error_Exp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UI_DisplayServices {
            get {
                return ((bool)(this["UI_DisplayServices"]));
            }
            set {
                this["UI_DisplayServices"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("150")]
        public uint UI_Item_Small_MaxWidth {
            get {
                return ((uint)(this["UI_Item_Small_MaxWidth"]));
            }
            set {
                this["UI_Item_Small_MaxWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.75")]
        public double UI_Item_Zoom_Away {
            get {
                return ((double)(this["UI_Item_Zoom_Away"]));
            }
            set {
                this["UI_Item_Zoom_Away"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("40")]
        public double UI_Item_Small_MaxHeight {
            get {
                return ((double)(this["UI_Item_Small_MaxHeight"]));
            }
            set {
                this["UI_Item_Small_MaxHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("220")]
        public double UI_Item_Medium_MaxWidth {
            get {
                return ((double)(this["UI_Item_Medium_MaxWidth"]));
            }
            set {
                this["UI_Item_Medium_MaxWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public double UI_Item_Big_MaxWidth {
            get {
                return ((double)(this["UI_Item_Big_MaxWidth"]));
            }
            set {
                this["UI_Item_Big_MaxWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("90")]
        public double UI_Item_Medium_MaxHeight {
            get {
                return ((double)(this["UI_Item_Medium_MaxHeight"]));
            }
            set {
                this["UI_Item_Medium_MaxHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("130")]
        public double UI_Item_Big_MaxHeight {
            get {
                return ((double)(this["UI_Item_Big_MaxHeight"]));
            }
            set {
                this["UI_Item_Big_MaxHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public double UI_Panel_ItemWidth {
            get {
                return ((double)(this["UI_Panel_ItemWidth"]));
            }
            set {
                this["UI_Panel_ItemWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public double UI_Panel_ItemHeight {
            get {
                return ((double)(this["UI_Panel_ItemHeight"]));
            }
            set {
                this["UI_Panel_ItemHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public double UI_Panel_Attraction {
            get {
                return ((double)(this["UI_Panel_Attraction"]));
            }
            set {
                this["UI_Panel_Attraction"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.65")]
        public double UI_Panel_Dampening {
            get {
                return ((double)(this["UI_Panel_Dampening"]));
            }
            set {
                this["UI_Panel_Dampening"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool XmppHttpPolling {
            get {
                return ((bool)(this["XmppHttpPolling"]));
            }
            set {
                this["XmppHttpPolling"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool XmppAutoReconnect {
            get {
                return ((bool)(this["XmppAutoReconnect"]));
            }
            set {
                this["XmppAutoReconnect"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int XmppReconnectSeconds {
            get {
                return ((int)(this["XmppReconnectSeconds"]));
            }
            set {
                this["XmppReconnectSeconds"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("250")]
        public int UI_DefChatWindowWidth {
            get {
                return ((int)(this["UI_DefChatWindowWidth"]));
            }
            set {
                this["UI_DefChatWindowWidth"] = value;
            }
        }
    }
}
