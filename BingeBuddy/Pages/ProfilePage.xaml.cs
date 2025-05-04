using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace BingeBuddy.Pages
{
    public partial class ProfilePage : ContentPage
    {
        // Holds user-generated status posts
        public ObservableCollection<string> UserPosts { get; set; }

        public ProfilePage()
        {
            InitializeComponent();

            UserPosts = new ObservableCollection<string>
            {
                "STAYED UP ALL NIGHT WATCHING SQUID GAME 2 WITH THE KIDS!!!"
            };

        }

        // Triggered when user taps 'Post'
        private void OnPostStatusClicked(object sender, EventArgs e)
        {
            string newPost = StatusEditor.Text?.Trim();

            if (string.IsNullOrEmpty(newPost))
            {
                DisplayAlert("Oops!", "Please enter a message before posting.", "OK");
                return;
            }

            // Add new post to the top of the list
            UserPosts.Insert(0, newPost);

            // Reset input field
            StatusEditor.Text = string.Empty;

            // Notify user
            DisplayAlert("Posted", "Your update has been shared!", "OK");

        }

        // Triggered when user taps 'Edit Profile'
        private void OnEditProfileClicked(object sender, EventArgs e)
        {
            // For now, just show an alert
            DisplayAlert("Edit Profile", "This would open the profile editing screen.", "OK");

            
        }
    }
}
