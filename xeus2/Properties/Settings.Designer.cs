﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xeus2.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("xeus")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("-3koblihy")]
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
    }
}