using BingeBuddy.Models;
using BingeBuddy.ViewModels;

namespace BingeBuddy.Pages
{
    public partial class AddMoviePage : ContentPage
    {
        public AddMoviePage(MovieViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
