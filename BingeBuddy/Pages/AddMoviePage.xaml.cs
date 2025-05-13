using BingeBuddy.Models;
using BingeBuddy.ViewModels;

namespace BingeBuddy.Pages
{
    public partial class AddMoviePage : ContentPage
    {
        public AddMoviePage()
        {
            InitializeComponent();
            BindingContext = new MovieViewModel();
        }
    }
}
