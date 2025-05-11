using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace BingeBuddy.Pages
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill out all fields.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // Save user data locally (for demo purposes only; not secure)
            Preferences.Set("SavedUsername", username);
            Preferences.Set("SavedEmail", email);
            Preferences.Set("SavedPassword", password);

            await DisplayAlert("Success", "Registration successful!", "OK");
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnLoginRedirectButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
