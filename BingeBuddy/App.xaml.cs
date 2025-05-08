using BingeBuddy.Pages;

namespace BingeBuddy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Start with the LoginPage
            MainPage = new NavigationPage(new LoginPage());
        }

        public static void NavigateToHomePage()
        {
            // Switch to AppShell with the navigation bar
            Current.MainPage = new AppShell();
        }
    }
}