namespace BingeBuddy.Pages;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    private async void OnLoginRedirectButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the LoginPage
        await Navigation.PushAsync(new LoginPage());
    }
}
