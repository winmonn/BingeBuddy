using BingeBuddy.Models;

namespace BingeBuddy.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void OnMovieCardTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Movie movie)
        {
            PopupImage.Source = movie.CoverPhoto;
            PopupTitle.Text = movie.Title;
            PopupSynopsis.Text = movie.Description;
            MoviePopup.IsVisible = true;
        }
    }

    private void ClosePopup_Clicked(object sender, EventArgs e)
    {
        MoviePopup.IsVisible = false;
    }

    private void OnPopupBackgroundTapped(object sender, TappedEventArgs e)
    {
        MoviePopup.IsVisible = false;
    }
}
