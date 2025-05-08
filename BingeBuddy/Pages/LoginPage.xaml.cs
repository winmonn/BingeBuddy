using BingeBuddy.Pages;

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
            // Perform login validation here
            bool isValidLogin = true; // Replace with actual login logic

            if (isValidLogin)
            {
                // Navigate to AppShell
                App.NavigateToHomePage();
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid credentials. Please try again.", "OK");
            }
        }

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the RegistrationPage
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
