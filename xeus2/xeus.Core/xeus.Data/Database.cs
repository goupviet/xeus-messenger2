using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using agsXMPP.Factory;
using agsXMPP.protocol.Base;
using agsXMPP.protocol.iq.disco;
using agsXMPP.Xml.Dom;
using xeus2.xeus.Core;
using Group=xeus2.xeus.Core.Group;

namespace xeus2.xeus.Data
{
    internal class Database
    {
        private static SQLiteConnection _connection = null;
        private const int _dbVersion = 1;

        private static string Path
        {
            get
            {
                return string.Format("{0}\\{1}", Storage.GetDbFolder(), "xeus.db");
            }
        }

        public static void OpenDatabase()
        {
            bool dbExisist = File.Exists(Path);

            _connection = new SQLiteConnection(string.Format("Data Source=\"{0}\"", Path));
            _connection.Open();

            if (!dbExisist)
            {
                CreateDatabase();
            }
            else
            {
                int dbVersion = GetVersion();

                if (dbVersion == 0)
                {
                    EventFatal eventError =
                        new EventFatal("Your 'xeus.db' is deprecated and can't be updated. Delete it and restart xeus");

                    Events.Instance.OnEvent(null, eventError);
                }

                if (dbVersion > _dbVersion)
                {
                    EventFatal eventError =
                        new EventFatal("Your 'xeus.db' is newer than your xeus-messenger. Install newest xeus version");
                    
                    Events.Instance.OnEvent(null, eventError);
                }
                else if (dbVersion  < _dbVersion)
                {
                    // possible updates
                }
            }
        }

        private static void CreateDatabase()
        {
            using (SQLiteCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "CREATE TABLE [Group] ([IsExpanded] INTEGER NOT NULL DEFAULT '0',"
                                  + "[Image] VARCHAR, "
                                  + "[Description] VARCHAR, "
                                  + "[Color] VARCHAR, "
                                  + "[Name] VARCHAR NOT NULL PRIMARY KEY UNIQUE);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE [System] ([Version] INTEGER NOT NULL DEFAULT '0');";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE [Message] ([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, "
                                  + "[From] VARCHAR NOT NULL, "
                                  + "[To] VARCHAR NOT NULL, "
                                  + "[DateTime] INTEGER NOT NULL, "
                                  + "[Body] VARCHAR NOT NULL, "
                                  + "[Type] VARCHAR NOT NULL, "
                                  + "FOREIGN KEY ([From]) REFERENCES [Contact]([Jid]) "
                                  + "FOREIGN KEY ([To]) REFERENCES [Contact]([Jid]));";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE [Contact] ([Jid] VARCHAR NOT NULL PRIMARY KEY UNIQUE, "
                                  + "[MetaId] INTEGER NOT NULL, "
                                  + "[CustomName] VARCHAR, "
                                  + "FOREIGN KEY ([MetaId]) REFERENCES [MetaContact]([Id]));";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE [MetaContact] ([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, "
                                  + "[CustomName] VARCHAR);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE [Recent] ([Position] INTEGER NOT NULL PRIMARY KEY UNIQUE, "
                                  + "[Jid] VARCHAR NOT NULL, "
                                  + "[DateTime] INTEGER NOT NULL, "
                                  + "[Type] VARCHAR NOT NULL);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE [CapsCache] ([Caps] VARCHAR NOT NULL PRIMARY KEY UNIQUE, "
                                  + "[Features] VARCHAR);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE [Error] ([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, "
                                  + "[Iq] VARCHAR, "
                                  + "[DateTime] INTEGER NOT NULL, "
                                  + "[Message] VARCHAR NOT NULL);";
                cmd.ExecuteNonQuery();
            }

            WriteVersion(_dbVersion, true);
        }

        public static void CloseDatabase()
        {
            _connection.Close();
        }

        static void WriteVersion(int version, bool isNew)
        {
            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    if (isNew)
                    {
                        command.CommandText = "INSERT INTO [System] ([Version]) VALUES (@version)";
                    }
                    else
                    {
                        command.CommandText = "UPDATE [System] SET [Version]=@version";
                    }

                    command.Parameters.Add(new SQLiteParameter("version", version));
                    command.ExecuteNonQuery();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
            
        }

        static int GetVersion()
        {
            int version = 0;

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Version] FROM [System]";

                    SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        version = (Int32) (Int64) reader["Version"];
                    }

                    reader.Close();
                }
            }

            catch (Exception)
            {
                // don't rise an event error
            }

            return version;
        }

        public static void SaveError(EventError error)
        {
            try
            {
                Dictionary<string, object> values = error.GetData();

                Insert(values, "Error", false, _connection);
            }

            catch
            {
                // NOT here
                // Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        public static void SaveRecent(Recent recent, int position)
        {
            try
            {
                Dictionary<string, object> values = recent.GetData();

                values.Add("Position", position);

                SaveOrUpdate(values, "Position", "Recent", false, _connection);
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        public static List<Recent> GetRecentItems(uint max)
        {
            List<Recent> recents = new List<Recent>();

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = string.Format(
                                            "SELECT * FROM [Recent] "
                                          + "ORDER BY [Recent].[DateTime] DESC "
                                          + "LIMIT {0};", max);

                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        recents.Add(new Recent(reader));
                    }

                    reader.Close();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }

            return recents;           
        }

        public static List<Message> GetMessages(IContact contact, uint maxMessages)
        {
            List<Message> messages = new List<Message>();

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = string.Format(
                                            "SELECT [Message].* FROM [Message] "
                                          + "INNER JOIN [Contact] ON ([Contact].[Jid]=[Message].[From] "
                                          + "OR [Contact].[Jid]=[Message].[To]) "
                                          + "AND [Contact].[Jid]=@jid AND [Type]='chat'"
                                          + "ORDER BY [Message].[DateTime] DESC "
                                          + "LIMIT {0};", maxMessages);

                    command.Parameters.Add(new SQLiteParameter("jid", contact.Jid.Bare));

                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Insert(0, new Message(reader));
                    }

                    reader.Close();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }

            return messages;
        }

        public static List<HeadlineMessage> GetHeadlines(uint maxMessages)
        {
            List<HeadlineMessage> messages = new List<HeadlineMessage>();

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = string.Format(
                                            "SELECT * FROM [Message] "
                                          + "WHERE [Type]='headline'"
                                          + "ORDER BY [Message].[DateTime] DESC "
                                          + "LIMIT {0};", maxMessages);

                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(new HeadlineMessage(reader));
                    }

                    reader.Close();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }

            return messages;
        }

        public static void SaveMessage(Message message)
        {
            try
            {
                Dictionary<string, object> values = message.GetData();

                Insert(values, "Message", false, _connection);
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        public static void SaveMessage(HeadlineMessage message)
        {
            try
            {
                Dictionary<string, object> values = message.GetData();

                Insert(values, "Message", false, _connection);
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        public static void SaveMessage(MucMessage message)
        {
            try
            {
                Dictionary<string, object> values = message.GetData();

                if (message.DateTime.DateTime.Value != null)
                {
                    if (!MucMessageExists(message.Sender, message.DateTime.DateTime.Value))
                    {
                        Insert(values, "Message", false, _connection);
                    }
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        static bool MucMessageExists(string sender, DateTime dateTime)
        {
            bool exists = false;

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Message] WHERE [Type]='muc' AND [From]=@sender AND [DateTime]=@dateTime";

                    command.Parameters.Add(new SQLiteParameter("sender", sender));
                    command.Parameters.Add(new SQLiteParameter("dateTime", dateTime));

                    SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        exists = true;
                    }

                    reader.Close();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }

            return exists;
        }

        public static Dictionary<string, DiscoInfo> GetCapsCache()
        {
            Dictionary<string, DiscoInfo>  capsCache = new Dictionary<string, DiscoInfo>(24);

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [CapsCache]";

                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        Document document = new Document();
                        document.LoadXml((string)reader["Features"]);

                        DiscoInfo info = document.RootElement as DiscoInfo;

                        if (info != null)
                        {
                            capsCache.Add((string) reader["Caps"], info);
                        }
                    }

                    reader.Close();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }

            return capsCache;
        }

        public static void SaveCapsCache(string caps, string features)
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>(2);
                values.Add("Caps", caps);
                values.Add("Features", features);

                SaveOrUpdate(values, "Caps", "CapsCache", false, _connection);
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        public static void SaveContact(Contact contact)
        {
            try
            {
                Dictionary<string, object> values = contact.GetData();

                SaveOrUpdate(values, "Jid", "Contact", false, _connection);
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        public static Contact GetContact(RosterItem rosterItem)
        {
            Contact contact = null;

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Contact] WHERE [Jid]=@jid";

                    command.Parameters.Add(new SQLiteParameter("jid", rosterItem.Jid.Bare));

                    SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        contact = new Contact(reader, rosterItem);
                    }

                    reader.Close();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }

            return contact;
        }

        public static void SaveMetaContact(MetaContact metaContact)
        {
            try
            {
                Dictionary<string, object> values = metaContact.GetData();

                metaContact.Id = SaveOrUpdate(values, "Id", "MetaContact", true, _connection);
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        public static MetaContact GetMetaContact(int id)
        {
            MetaContact metaContact = null;

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [MetaContact] WHERE [Id]=@id";

                    command.Parameters.Add(new SQLiteParameter("id", id));

                    SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        metaContact = new MetaContact(reader);
                    }

                    reader.Close();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }

            return metaContact;
        }

        public static void StoreGroups(ObservableCollectionDisp<Core.Group> groups)
        {
            try
            {
                using (SQLiteTransaction transaction = _connection.BeginTransaction())
                {
                    foreach (Group group in groups)
                    {
                        Dictionary<string, object> values = group.GetData();

                        SaveOrUpdate(values, "Name", "Group", false, _connection);
                    }

                    transaction.Commit();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        public static void ReadGroups(ObservableCollectionDisp<Group> groups)
        {
            groups.Clear();

            try
            {
                using (SQLiteCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Group]";

                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        groups.Add(new Group(reader));
                    }

                    reader.Close();
                }
            }

            catch (Exception e)
            {
                Events.Instance.OnEvent(e, new EventError(e.Message, null));
            }
        }

        /*
		public void SaveRosterItem( RosterItem item )
		{
			if ( item.IsService )
			{
				return ;
			}

			try
			{
				if ( !item.IsInDatabase )
				{
					Insert( item.GetData(), "RosterItem", false, _connection ) ;
					item.IsInDatabase = true ;
					item.IsDirty = false ;
				}
				else if ( item.IsDirty )
				{
					Update( item.GetData(), "Key", "RosterItem", _connection ) ;
					item.IsDirty = false ;
				}
			}

			catch ( Exception e )
			{
				Client.Instance.Log( "Error writing roster items: {0}", e.Message ) ;
			}
		}

		public void StoreRosterItems( ObservableCollectionDisp< RosterItem > rosterItems )
		{
			lock ( rosterItems._syncObject )
			{
				using ( SQLiteTransaction transaction = _connection.BeginTransaction() )
				{
					foreach ( RosterItem item in rosterItems )
					{
						SaveRosterItem( item ) ;
					}

					transaction.Commit();
				}
			}
		}

		public void DeleteRosterItem( RosterItem rosterItem )
		{
			Delete( "RosterItem", "Key", rosterItem.Key, _connection ) ;
		}

		void Delete( string table, string keyField, object keyValue, SQLiteConnection connection )
		{
			using ( SQLiteCommand commandDelete = connection.CreateCommand() )
			{
				StringBuilder queryDelete = new StringBuilder() ;

				queryDelete.AppendFormat( "DELETE FROM [{0}] WHERE [{1}]=@{1}", table, keyField ) ;
				
				commandDelete.Parameters.Add( new SQLiteParameter( keyField, keyValue ) ) ;

				commandDelete.CommandText = queryDelete.ToString() ;
				commandDelete.ExecuteNonQuery() ;
			}
		}
		*/

        private static Int32 Insert(IEnumerable<KeyValuePair<string, object>> values, string table, bool readAutoId,
                                    SQLiteConnection connection)
        {
            using (SQLiteCommand commandUpdate = connection.CreateCommand())
            {
                StringBuilder queryUpdate = new StringBuilder();

                queryUpdate.AppendFormat("INSERT INTO [{0}] (", table);

                bool isFirst = true;

                foreach (KeyValuePair<string, object> pair in values)
                {
                    if (pair.Key == "Id")
                    {
                        //don't insert auto id
                        continue;
                    }

                    if (!isFirst)
                    {
                        queryUpdate.Append(",");
                    }

                    isFirst = false;

                    queryUpdate.AppendFormat("[{0}]", pair.Key);
                }

                queryUpdate.Append(") VALUES (");

                isFirst = true;

                foreach (KeyValuePair<string, object> pair in values)
                {
                    if (pair.Key == "Id")
                    {
                        //don't insert auto id
                        continue;
                    }

                    if (!isFirst)
                    {
                        queryUpdate.Append(",");
                    }

                    isFirst = false;

                    queryUpdate.AppendFormat("@{0}", pair.Key);

                    commandUpdate.Parameters.Add(new SQLiteParameter(pair.Key, pair.Value));
                }

                queryUpdate.Append(")");

                commandUpdate.CommandText = queryUpdate.ToString();
                commandUpdate.ExecuteNonQuery();
            }

            if (readAutoId)
            {
                return GetlastRowId(connection);
            }
            else
            {
                return 0;
            }
        }

        private static void Update(IDictionary<string, object> values, string keyField, string table,
                                   SQLiteConnection connection)
        {
            using (SQLiteCommand commandUpdate = connection.CreateCommand())
            {
                StringBuilder queryUpdate = new StringBuilder();

                queryUpdate.AppendFormat("UPDATE [{0}] SET ", table);

                bool isFirst = true;

                foreach (KeyValuePair<string, object> pair in values)
                {
                    if (string.Compare(keyField, pair.Key, true) == 0)
                    {
                        continue;
                    }

                    if (!isFirst)
                    {
                        queryUpdate.Append(",");
                    }

                    isFirst = false;

                    queryUpdate.AppendFormat("[{0}]=@{0}", pair.Key);

                    commandUpdate.Parameters.Add(new SQLiteParameter(pair.Key, pair.Value));
                }

                queryUpdate.AppendFormat(" WHERE [{0}]=@{0}", keyField);
                commandUpdate.Parameters.Add(new SQLiteParameter(keyField, values[keyField]));

                commandUpdate.CommandText = queryUpdate.ToString();
                commandUpdate.ExecuteNonQuery();
            }
        }

        private static int SaveOrUpdate(IDictionary<string, object> values, string keyField, string table,
                                        bool readAutoId,
                                        SQLiteConnection connection)
        {
            bool exists = false;

            int id = 0;

            if (keyField != null)
            {
                StringBuilder query = new StringBuilder();

                query.AppendFormat("SELECT * FROM [{0}] WHERE [{1}]=@keyparam", table, keyField);

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query.ToString();

                    command.Parameters.Add(new SQLiteParameter("keyparam", values[keyField]));

                    if (readAutoId)
                    {
                        id = (int) values[keyField];
                    }

                    SQLiteDataReader reader = command.ExecuteReader();

                    exists = reader.HasRows;

                    reader.Close();
                }
            }

            if (exists)
            {
                Update(values, keyField, table, connection);
            }
            else
            {
                id = Insert(values, table, readAutoId, connection);
            }

            return id;
        }

        private static int GetlastRowId(SQLiteConnection connection)
        {
            int id = 0;

            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT last_insert_rowid()";

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    id = (Int32) (Int64) reader[0];
                }

                reader.Close();
            }

            return id;
        }
    }
}