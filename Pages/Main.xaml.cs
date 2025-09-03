using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using System.Text.Json;
using Wpf.Ui.Controls;
using Wpf.Ui;
using System.Linq.Expressions;

namespace Roblox_Moderation_Application_WPF.Pages
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : ScrollViewer
    {

        public string? ApiKey;
        public string? UniverseID;
        private string? UsernameVar;

        public Main()
        {
            InitializeComponent();

            var connection = new SqliteConnection($"Data Source = {RobloxDB.CombinedPath}");
            var commandString = $"SELECT * FROM {RobloxDB.TableName}";

            var command = new SqliteCommand(commandString, connection);
            connection.Open();

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ApiKey = reader.GetString(0);
                    UniverseID = reader.GetString(1);
                }
            }
        }
        
        private async void IDChanged(object sender, TextChangedEventArgs args)
        {
            ErrorInfo.IsOpen = false;

            Wpf.Ui.Controls.TextBox UserIDTextBox = (Wpf.Ui.Controls.TextBox)this.Resources["UserIDTextBox"];
            Info.UserID = UserIDTextBox.Text;

            var RobloxClient = new HttpClient();
            RobloxClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);

            var response = await RobloxClient.GetAsync($"https://apis.roblox.com/cloud/v2/users/{Info.UserID}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var parsed = JsonDocument.Parse(content);
                var rootElement = parsed.RootElement;

                var displayName = rootElement.GetProperty("displayName").ToString();
                var username = rootElement.GetProperty("name").ToString();
                var id = rootElement.GetProperty("id").ToString();

                DisplayNameText.Text = displayName;
                UsernameText.Text = username;
                UserIDText.Text = id;

                UsernameVar = username;
            } else
            {
                ErrorInfo.IsOpen = true;
                ErrorInfo.Message = "An error occured while trying to get the user information.\nError message: " + response.StatusCode.ToString();
            }

            var thumbnailResponse = await RobloxClient.GetAsync($"https://apis.roblox.com/cloud/v2/users/{Info.UserID}:generateThumbnail?size=420&format=PNG&shape=SQUARE");

            if (thumbnailResponse.IsSuccessStatusCode)
            {
                var thumbResp = await thumbnailResponse.Content.ReadAsStringAsync();
                var parsedThumb = JsonDocument.Parse(thumbResp);
                var rootThumb = parsedThumb.RootElement;

                var imageUri = rootThumb.GetProperty("response").GetProperty("imageUri").ToString();

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(imageUri);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                ProfileImg.Source = bitmapImage;
            } else
            {
                ErrorInfo.IsOpen = true;
                ErrorInfo.Message = "An error occured while trying to get the user thumbnail.\nError message: " + thumbnailResponse.StatusCode.ToString();
            }
        }

        private void PermanentChecked(object sender, RoutedEventArgs e)
        {
            DurationCard.IsEnabled = PermanentCheckBox.IsChecked == true ? false : true;
            Info.Permanent = PermanentCheckBox.IsChecked;
        }

        private void DurationChanged(object sender, RoutedEventArgs e)
        {
            Info.Duration = ((Wpf.Ui.Controls.TextBox)this.Resources["DurationTextBox"]).Text;
        }

        public async void BanClicked(object sender, RoutedEventArgs e)
        {
            SuccessInfo.IsOpen = false;
            ErrorInfo.IsOpen = false;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);

            var durationTable = Info.Duration + "s";
            if (Info.Permanent == true)
            {
                durationTable = null;
            }

            var json = JsonConvert.SerializeObject(new
            {
                gameJoinRestriction = new
                {
                    active = true,
                    duration = durationTable,
                    displayReason = ((Wpf.Ui.Controls.TextBox)this.Resources["ReasonTextBox"]).Text,
                    privateReason = ((Wpf.Ui.Controls.TextBox)this.Resources["PrivReasonTextBox"]).Text,
                    excludeAltAccounts = true,
                }
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PatchAsync($"https://apis.roblox.com/cloud/v2/universes/{UniverseID}/user-restrictions/{Info.UserID}", content);

            if (response.IsSuccessStatusCode)
            {
                SuccessInfo.IsOpen = true;
                SuccessInfo.Message = "Successfully banned " + UsernameVar;
            } else
            {
                ErrorInfo.IsOpen = true;
                ErrorInfo.Message = "An error occured while trying to ban " + UsernameVar + ".\nError message: " + response.StatusCode.ToString();
            }
        }

        public async void UnbanClicked(object sender, RoutedEventArgs e)
        {
            SuccessInfo.IsOpen = false;
            ErrorInfo.IsOpen = false;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);

            var json = JsonConvert.SerializeObject(new
            {
                gameJoinRestriction = new
                {
                    active = false
                }
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PatchAsync($"https://apis.roblox.com/cloud/v2/universes/{UniverseID}/user-restrictions/{Info.UserID}", content);

            if (response.IsSuccessStatusCode)
            {
                SuccessInfo.IsOpen = true;
                SuccessInfo.Message = "Successfully unbanned " + UsernameVar + ".";
            } else
            {
                ErrorInfo.IsOpen = true;
                ErrorInfo.Message = "An error occured while trying to unban " + UsernameVar + ".\nError message: " + response.StatusCode.ToString();
            }
        }
    }
}
