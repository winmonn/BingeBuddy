using BingeBuddy.Models;
using BingeBuddy.ViewModels;

namespace BingeBuddy.Pages
{
    public partial class MovieDetailsPage : ContentPage
    {
        public MovieDetailsPage(Movie movie)
        {
            InitializeComponent();
            BindingContext = new MovieDetailsViewModel(movie);
        }
    }
}
