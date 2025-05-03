using Microsoft.Maui.Controls;
using BingeBuddy.ViewModels; // Updated namespace

namespace BingeBuddy; // Updated namespace

public partial class MainPage : ContentPage
{
    private MovieViewModel ViewModel => BindingContext as MovieViewModel;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnAddMovieClicked(object sender, EventArgs e)
    {
        ViewModel?.AddMovie();
    }
}
