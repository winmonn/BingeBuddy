using BingeBuddy.ViewModels;

namespace BingeBuddy.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage(MovieViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        // When the Profile button is clicked, navigate to the ProfilePage
        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the ProfilePage using Navigation.PushAsync
            await Navigation.PushAsync(new ProfilePage());
        }
    }
}
