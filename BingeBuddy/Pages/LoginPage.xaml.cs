using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

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
            string enteredUsername = usernameEntry.Text?.Trim();
            string enteredPassword = passwordEntry.Text;

            string savedUsername = Preferences.Get("SavedUsername", string.Empty);
            string savedPassword = Preferences.Get("SavedPassword", string.Empty);

            if (enteredUsername == savedUsername && enteredPassword == savedPassword)
            {
                // Successful login
                await DisplayAlert("Welcome", $"Hello, {enteredUsername}!", "OK");

                // Navigate to the Home Page (AppShell method or direct page)
                App.NavigateToHomePage(); // You can replace with Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid credentials. Please try again.", "OK");
            }
        }

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
