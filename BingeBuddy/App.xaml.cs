using BingeBuddy.Pages; // Add this namespace if ProfilePage is defined in the Pages folder

namespace BingeBuddy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            // Wrap ProfilePage in NavigationPage to enable navigation
            return new Window(new NavigationPage(new ProfilePage()));
        }
    }
}
