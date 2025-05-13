using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace BingeBuddy.Pages
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            string enteredEmail = emailEntry.Text?.Trim();
            string enteredUsername = usernameEntry.Text?.Trim();
            string newPassword = newPasswordEntry.Text;
            string repeatPassword = repeatPasswordEntry.Text;

            string savedEmail = Preferences.Get("SavedEmail", string.Empty);
            string savedUsername = Preferences.Get("SavedUsername", string.Empty);

            if (enteredEmail == savedEmail && enteredUsername == savedUsername)
            {
                if (string.IsNullOrWhiteSpace(newPassword) || newPassword != repeatPassword)
                {
                    await DisplayAlert("Error", "New passwords do not match or are empty.", "OK");
                    return;
                }

                Preferences.Set("SavedPassword", newPassword);
                await DisplayAlert("Success", "Password successfully reset.", "OK");
                await Navigation.PopAsync(); // Go back to login page
            }
            else
            {
                await DisplayAlert("Error", "Email or username doesn't match our records.", "OK");
            }
        }
    }
}
