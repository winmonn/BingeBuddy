using Microsoft.Maui.Controls;

namespace BingeBuddy.Pages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        ShowActivityTab(); // default tab
    }

    private void OnTabClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        switch (button.StyleId)
        {
            case "ActivityTab":
                ShowActivityTab();
                break;
            case "ListsTab":
                ShowListsTab();
                break;
            case "FilmsTab":
                ShowFilmsTab();
                break;
            case "WatchLaterTab":
                ShowWatchLaterTab();
                break;
        }
    }

    private void ShowActivityTab()
    {
        TabContent.Content = new StackLayout
        {
            Children =
            {
                new Frame
                {
                    Content = new Label
                    {
                        Text = "Recent review: ★★★★☆\nThe Creator - Sci-fi/Action\n\"Cinematic masterpiece...\"",
                        FontSize = 14
                    },
                    BorderColor = Colors.LightGray,
                    CornerRadius = 10,
                    Padding = 10
                },
                new Label { Text = "Recent lists", FontAttributes = FontAttributes.Bold, FontSize = 16 },
                new Label { Text = "• BEST OF 2024 (SO FARR) - 18 films" },
                new Label { Text = "• Must-Watch Dramas - 42 films" }
            }
        };
    }

    private void ShowListsTab()
    {
        TabContent.Content = new Label { Text = "User-created lists go here...", FontSize = 14 };
    }

    private void ShowFilmsTab()
    {
        TabContent.Content = new Label { Text = "Rated/Watched Films section...", FontSize = 14 };
    }

    private void ShowWatchLaterTab()
    {
        TabContent.Content = new Label { Text = "Watch Later items listed here...", FontSize = 14 };
    }
}
