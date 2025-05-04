using BingeBuddy.Pages; // Make sure this matches your namespace
using Microsoft.Maui.Controls;

namespace BingeBuddy.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Navigate to MainPage
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the RegistrationPage
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
