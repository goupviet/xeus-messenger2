﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1378
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
    }
}
