using Microsoft.Maui.Controls;
using BingeBuddy.ViewModels;

namespace BingeBuddy;

public partial class MainPage : ContentPage
{
    private MovieViewModel ViewModel => BindingContext as MovieViewModel;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MovieViewModel(); // Ensures ViewModel is set in code-behind
    }

    private void OnAddMovieClicked(object sender, EventArgs e)
    {
        ViewModel?.AddMovie();
    }
}
