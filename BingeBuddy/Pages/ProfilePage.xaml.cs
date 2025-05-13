using Microsoft.Maui.Controls;
using BingeBuddy.ViewModels;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;

namespace BingeBuddy.Pages;

public partial class ProfilePage : ContentPage
{
    private ObservableCollection<UserList> userLists = new();
    private ObservableCollection<string> watchLaterMovies = new(); // Watch Later list

    private readonly string listsFilePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "userLists.json");
    private readonly string watchLaterFilePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "watchLater.json");

    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = new MovieViewModel();
        LoadUserLists();
        LoadWatchLater();
        ShowActivityTab();
    }

    private void OnTabClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            switch (button.StyleId)
            {
                case "ActivityTab": ShowActivityTab(); break;
                case "ListsTab": ShowListsTab(); break;
                case "FilmsTab": ShowFilmsTab(); break;
                case "WatchLaterTab": ShowWatchLaterTab(); break;
            }
        }
    }

    // ----------------------------
    // Activity Tab
    // ----------------------------

    private void ShowActivityTab()
    {
        TabContent.Content = new VerticalStackLayout
        {
            Spacing = 15,
            Children =
            {
                new Label { Text = "Recent Reviews", FontSize = 16, FontAttributes = FontAttributes.Bold },
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
                                FontSize = 13
                            }
                        }
                    }
                },
                new Label { Text = "Recent Lists", FontSize = 16, FontAttributes = FontAttributes.Bold, Margin = new Thickness(0,10,0,0) },
                new Label { Text = "• BEST OF 2024 (SO FARR) – 18 films", FontSize = 13 },
                new Label { Text = "• Must-Watch Dramas – 42 films", FontSize = 13 }
            }
        };
    }

    // ----------------------------
    // Lists Tab
    // ----------------------------

    private void ShowListsTab()
    {
        var stack = new VerticalStackLayout { Padding = 10, Spacing = 10 };

        var addButton = new Button
        {
            Text = "➕ Create New List",
            BackgroundColor = Colors.LightGray,
            TextColor = Colors.Black,
            CornerRadius = 12,
            Padding = 10,
            FontAttributes = FontAttributes.Bold
        };
        addButton.Clicked += OnCreateListClicked;
        stack.Children.Add(addButton);

        foreach (var list in userLists)
        {
            var frame = new Frame
            {
                Padding = 12,
                CornerRadius = 10,
                BorderColor = Colors.LightGray,
                Content = new VerticalStackLayout
                {
                    Spacing = 4,
                    Children =
                    {
                        new Label { Text = list.Title, FontSize = 15, FontAttributes = FontAttributes.Bold },
                        new Label { Text = $"{list.FilmCount} films", FontSize = 13, TextColor = Colors.Gray },
                        new HorizontalStackLayout
                        {
                            Spacing = 10,
                            Children =
                            {
                                new Button
                                {
                                    Text = "✏️ Edit",
                                    FontSize = 12,
                                    BackgroundColor = Colors.Transparent,
                                    TextColor = Colors.Blue,
                                    Command = new Command(async () => await EditListAsync(list))
                                },
                                new Button
                                {
                                    Text = "🗑 Delete",
                                    FontSize = 12,
                                    BackgroundColor = Colors.Transparent,
                                    TextColor = Colors.Red,
                                    Command = new Command(async () => await DeleteListAsync(list))
                                }
                            }
                        }
                    }
                }
            };
            stack.Children.Add(frame);
        }

        TabContent.Content = new ScrollView { Content = stack };
    }

    private async void OnCreateListClicked(object sender, EventArgs e)
    {
        string title = await DisplayPromptAsync("New List", "Enter list title:");
        if (!string.IsNullOrWhiteSpace(title))
        {
            userLists.Insert(0, new UserList { Title = title, FilmCount = 0 });
            await SaveUserListsAsync();
            ShowListsTab();
        }
    }

    private async Task EditListAsync(UserList list)
    {
        string newTitle = await DisplayPromptAsync("Edit List", "Edit list title:", initialValue: list.Title);
        if (!string.IsNullOrWhiteSpace(newTitle))
        {
            list.Title = newTitle;
            await SaveUserListsAsync();
            ShowListsTab();
        }
    }

    private async Task DeleteListAsync(UserList list)
    {
        bool confirm = await DisplayAlert("Delete List", $"Are you sure you want to delete '{list.Title}'?", "Yes", "Cancel");
        if (confirm)
        {
            userLists.Remove(list);
            await SaveUserListsAsync();
            ShowListsTab();
        }
    }

    private async Task SaveUserListsAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(userLists);
            await File.WriteAllTextAsync(listsFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving lists: " + ex.Message);
        }
    }

    private void LoadUserLists()
    {
        try
        {
            if (File.Exists(listsFilePath))
            {
                var json = File.ReadAllText(listsFilePath);
                var loaded = JsonSerializer.Deserialize<ObservableCollection<UserList>>(json);
                if (loaded != null)
                    userLists = loaded;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading lists: " + ex.Message);
        }
    }

    // ----------------------------
    // Films Tab
    // ----------------------------

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

                var poster = new Image { HeightRequest = 60, WidthRequest = 40, Aspect = Aspect.AspectFill };
                poster.SetBinding(Image.SourceProperty, "Poster");

                var titleLabel = new Label { FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#333") };
                titleLabel.SetBinding(Label.TextProperty, "Title");

                var genreLabel = new Label { FontSize = 13, TextColor = Color.FromArgb("#666") };
                genreLabel.SetBinding(Label.TextProperty, "Genre");

                var ratingLabel = new Label
                {
                    FontSize = 14,
                    TextColor = Colors.Gold,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start
                };
                ratingLabel.SetBinding(Label.TextProperty, "Rating");

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

    // ----------------------------
    // Watch Later Tab
    // ----------------------------

    private void ShowWatchLaterTab()
    {
        var stack = new VerticalStackLayout { Padding = 10, Spacing = 10 };

        var addButton = new Button
        {
            Text = "➕ Add to Watch Later",
            BackgroundColor = Colors.LightGray,
            TextColor = Colors.Black,
            CornerRadius = 12,
            FontAttributes = FontAttributes.Bold
        };
        addButton.Clicked += async (s, e) =>
        {
            string title = await DisplayPromptAsync("Watch Later", "Enter movie title to add:");
            if (!string.IsNullOrWhiteSpace(title))
            {
                watchLaterMovies.Insert(0, title);
                await SaveWatchLaterAsync();
                ShowWatchLaterTab();
            }
        };
        stack.Children.Add(addButton);

        foreach (var movie in watchLaterMovies)
        {
            var row = new HorizontalStackLayout
            {
                Spacing = 10,
                Children =
                {
                    new Label { Text = "• " + movie, FontSize = 14, VerticalOptions = LayoutOptions.Center },
                    new Button
                    {
                        Text = "❌",
                        FontSize = 12,
                        BackgroundColor = Colors.Transparent,
                        TextColor = Colors.Red,
                        Command = new Command(async () =>
                        {
                            bool confirm = await DisplayAlert("Remove", $"Remove '{movie}' from Watch Later?", "Yes", "Cancel");
                            if (confirm)
                            {
                                watchLaterMovies.Remove(movie);
                                await SaveWatchLaterAsync();
                                ShowWatchLaterTab();
                            }
                        })
                    }
                }
            };
            stack.Children.Add(row);
        }

        TabContent.Content = new ScrollView { Content = stack };
    }

    private async Task SaveWatchLaterAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(watchLaterMovies);
            await File.WriteAllTextAsync(watchLaterFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving Watch Later: " + ex.Message);
        }
    }

    private void LoadWatchLater()
    {
        try
        {
            if (File.Exists(watchLaterFilePath))
            {
                var json = File.ReadAllText(watchLaterFilePath);
                var loaded = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
                if (loaded != null)
                    watchLaterMovies = loaded;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading Watch Later: " + ex.Message);
        }
    }
}

// ----------------------------
// UserList model
// ----------------------------

public class UserList
{
    public string Title { get; set; }
    public int FilmCount { get; set; }
}