using System;
using Microsoft.Maui.Controls;

namespace BingeBuddy.Pages
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void OnEditProfileClicked(object sender, EventArgs e)
        {
            // Placeholder logic for editing profile
            DisplayAlert("Edit Profile", "Edit Profile button clicked.", "OK");
        }

        private void OnPostStatusClicked(object sender, EventArgs e)
        {
            // Placeholder logic for posting status
            DisplayAlert("Post Status", "Status posted successfully!", "OK");
        }
    }
}
