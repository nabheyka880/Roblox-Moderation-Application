using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace Roblox_Moderation_Application_WPF.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : ScrollViewer
    {
        private Wpf.Ui.Controls.TextBox ApiKeyBox;
        private Wpf.Ui.Controls.TextBox UniverseIDBox;

        public SettingsPage()
        {
            InitializeComponent();

            ApiKeyBox = (Wpf.Ui.Controls.TextBox)this.Resources["ApiKeyTextBox"];
            UniverseIDBox = (Wpf.Ui.Controls.TextBox)this.Resources["UniverseIDText"];

            var SqliteConnection = new SqliteConnection($"Data Source = {RobloxDB.CombinedPath}");
            var stringCommand = "SELECT * FROM " + RobloxDB.TableName;

            var sqliteCommand = new SqliteCommand(stringCommand, SqliteConnection);

            SqliteConnection.Open();

            var reader = sqliteCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ApiKeyBox.Text = reader.GetString(0);
                    UniverseIDBox.Text = reader.GetString(1);
                }
            }
        }

        private void SavedSettings(object sender, RoutedEventArgs e)
        {
            var ApiKey = ApiKeyBox.Text;
            var UniverseID = UniverseIDBox.Text;

            SuccessInfo.IsOpen = false;

            try
            {
                var connectionThing = new SqliteConnection($"Data Source = {RobloxDB.CombinedPath}");
                var stringCommand = $"INSERT INTO {RobloxDB.TableName}(ApiKey, UniverseID) VALUES (@KEY, @ID)";

                connectionThing.Open();

                var command = new SqliteCommand(stringCommand, connectionThing);

                command.Parameters.AddWithValue("@KEY", ApiKey);
                command.Parameters.AddWithValue("@ID", UniverseID);

                command.ExecuteNonQuery();

                SuccessInfo.IsOpen = true;
            } catch (Exception er)
            {
                var messageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "Saving Error",
                    Content = "An error occured while trying to save the settings.\n" + er.Message,
                    PrimaryButtonText = "Ok",
                    CloseButtonText = "Close"
                };

                messageBox.ShowDialogAsync();
            }
        }
    }
}
