using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace Roblox_Moderation_Application_WPF
{
    class RobloxDB
    {
        public static SqliteConnection? Connection;
        public static string DatabaseName = "ROBLOXDATA.db";
        public static string TableName = "INFORMATION";
        public static string CombinedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RMA", DatabaseName);

        public static void Initialize()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(CombinedPath));

                var connectionString = new SqliteConnectionStringBuilder{
                    DataSource = CombinedPath,
                    Mode = SqliteOpenMode.ReadWriteCreate
                }.ToString();

                Connection = new SqliteConnection(connectionString);
                Connection.Open();

                var stringCommand = $"CREATE TABLE IF NOT EXISTS {TableName} (ApiKey TEXT, UniverseID TEXT)";

                var sqliteCommand = new SqliteCommand(stringCommand, Connection);

                sqliteCommand.ExecuteNonQuery();
            } catch (Exception er)
            {
                var messageBox = new Wpf.Ui.Controls.MessageBox{ 
                    Title = "Database Error",
                    Content = "An error occured while trying to Initialize the Database.\n" + er.Message,
                    PrimaryButtonText = "Ok",
                    SecondaryButtonText = "what is supposed to be here"
                };

                messageBox.ShowDialogAsync();
            }
        }
    }
}
