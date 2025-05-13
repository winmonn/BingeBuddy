using Microsoft.Maui.Controls;
using BingeBuddy.ViewModels;
using Microsoft.Maui.Controls.Shapes;

namespace BingeBuddy.Pages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = new MovieViewModel();  // Bind the page to the MovieViewModel
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
                new Label { Text = "• Must-Watch Dramas – 42 films", FontSize = 13 },

                // Bind the filtered movies to the UI dynamically
                new CollectionView
                {
                    ItemsSource = ((MovieViewModel)BindingContext).FilteredMovies,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        var movieLabel = new Label();
                        movieLabel.SetBinding(Label.TextProperty, "Title");
                        return movieLabel;
                    })
                }
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
        if (BindingContext is MovieViewModel vm)
        {
            vm.StatusFilter = "Completed";
        }

        TabContent.Content = new CollectionView
        {
            Margin = new Thickness(10),
            ItemsSource = ((MovieViewModel)BindingContext).FilteredMovies,
            ItemTemplate = new DataTemplate(() =>
            {
                var border = new Border
                {
                    Padding = 15,
                    Margin = new Thickness(0, 10),
                    BackgroundColor = Color.FromArgb("#FFFFFF"),
                    StrokeShape = new RoundRectangle { CornerRadius = 12 },
                    Shadow = new Shadow
                    {
                        Brush = Colors.Black,
                        Opacity = 0.2f,
                        Offset = new Point(3, 3),
                        Radius = 5
                    }
                };

                var grid = new Grid
                {
                    ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                    RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                    ColumnSpacing = 10
                };

                var poster = new Image
                {
                    HeightRequest = 60,
                    WidthRequest = 40,
                    Aspect = Aspect.AspectFill
                };
                poster.SetBinding(Image.SourceProperty, "Poster"); // Ensure this exists in your Movie model

                var titleLabel = new Label
                {
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#333")
                };
                titleLabel.SetBinding(Label.TextProperty, "Title");

                var genreLabel = new Label
                {
                    FontSize = 13,
                    TextColor = Color.FromArgb("#666")
                };
                genreLabel.SetBinding(Label.TextProperty, "Genre");

                var ratingLabel = new Label
                {
                    FontSize = 14,
                    TextColor = Colors.Gold,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start
                };
                ratingLabel.SetBinding(Label.TextProperty, "Rating"); // e.g. ★★★★☆

                // Grid placement
                grid.Add(poster, 0, 0);
                Grid.SetRowSpan(poster, 3);

                grid.Add(titleLabel, 1, 0);
                grid.Add(genreLabel, 1, 1);
                grid.Add(ratingLabel, 1, 2);

                border.Content = grid;
                return border;
            })
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
