using System;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace JsonProject
{
    public partial class MainPage : ContentPage
    {
        private string profileFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profile.json");

        public MainPage()
        {
            InitializeComponent();

            // Load existing profile data (if any)
            LoadProfile();
        }

        private void LoadProfile()
        {
            try
            {
                if (File.Exists(profileFilePath))
                {
                    var json = File.ReadAllText(profileFilePath);
                    var profile = JsonSerializer.Deserialize<Profile>(json);

                    if (profile != null)
                    {
                        nameEntry.Text = profile.Name;
                        surnameEntry.Text = profile.Surname;
                        emailEntry.Text = profile.Email;
                        bioEditor.Text = profile.Bio;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file access errors)
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var profile = new Profile
            {
                Name = nameEntry.Text,
                Surname = surnameEntry.Text,
                Email = emailEntry.Text,
                Bio = bioEditor.Text
            };

            var json = JsonSerializer.Serialize(profile);

            try
            {
                using var stream = new StreamWriter(profileFilePath);
                await stream.WriteAsync(json);

                // Clear input fields after successfully saving profile
                nameEntry.Text = string.Empty;
                surnameEntry.Text = string.Empty;
                emailEntry.Text = string.Empty;
                bioEditor.Text = string.Empty;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file access errors)
            }
        }

        private void LoadProfileButton_Clicked(object sender, EventArgs e)
        {
            // Load profile when the "Load Profile" button is clicked
            LoadProfile();
        }
    }

    public class Profile
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }
}
