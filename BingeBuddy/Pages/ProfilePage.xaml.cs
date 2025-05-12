using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace BingeBuddy.Pages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        ShowActivityTab(); // Default tab
    }

    private void OnTabClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
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
    }

    private void ShowActivityTab()
    {
        TabContent.Content = new VerticalStackLayout
        {
            Spacing = 15,
            Children =
            {
                new Label
                {
                    Text = "Recent Reviews",
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold
                },

                new Frame
                {
                    CornerRadius = 12,
                    BorderColor = Colors.LightGray,
                    Padding = 12,
                    Content = new VerticalStackLayout
                    {
                        Spacing = 4,
                        Children =
                        {
                            new Label { Text = "The Creator (2023)", FontAttributes = FontAttributes.Bold, FontSize = 14 },
                            new Label { Text = "Genre: Sci-Fi / Action", FontSize = 12, TextColor = Colors.Gray },
                            new Label { Text = "★★★★☆", FontSize = 14, TextColor = Colors.Gold },
                            new Label
                            {
                                Text = "\"A cinematic masterpiece with breathtaking visuals and thought-provoking AI narrative.\"",
                                FontSize = 13,
                                TextColor = Colors.Black
                            }
                        }
                    }
                },

                new Label
                {
                    Text = "Recent Lists",
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(0, 10, 0, 0)
                },

                new Label { Text = "• BEST OF 2024 (SO FARR) – 18 films", FontSize = 13 },
                new Label { Text = "• Must-Watch Dramas – 42 films", FontSize = 13 }
            }
        };
    }

    private void ShowListsTab()
    {
        TabContent.Content = new VerticalStackLayout
        {
            Children =
            {
                new Label { Text = "User-created lists go here...", FontSize = 14 }
            }
        };
    }

    private void ShowFilmsTab()
    {
        TabContent.Content = new VerticalStackLayout
        {
            Children =
            {
                new Label { Text = "Rated / Watched Films section...", FontSize = 14 }
            }
        };
    }

    private void ShowWatchLaterTab()
    {
        TabContent.Content = new VerticalStackLayout
        {
            Children =
            {
                new Label { Text = "Watch Later items listed here...", FontSize = 14 }
            }
        };
    }
}
    