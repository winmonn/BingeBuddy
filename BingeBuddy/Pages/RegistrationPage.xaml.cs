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
            string username = usernameEntry.Text?.Trim();
            string email = emailEntry.Text?.Trim();
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
    string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill out all fields.", "OK");
                return;
            }

            if (!email.EndsWith(".com") || !email.Contains("@"))
            {
                await DisplayAlert("Invalid Email", "Email must be a valid email address.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // Save user data locally
            Preferences.Set("SavedUsername", username);
            Preferences.Set("SavedPassword", password);
            Preferences.Set("SavedEmail", email);

            await DisplayAlert("Success", "Account created successfully!", "OK");

            // Navigate to login page
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnLoginRedirectButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
