using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using BingeBuddy.Models;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Maui.Graphics;

namespace BingeBuddy.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public Color InProgressButtonColor => StatusFilter == "Progress" ? Color.FromArgb("#22313A") : Colors.Transparent;
        public Color CompletedButtonColor => StatusFilter == "Completed" ? Color.FromArgb("#22313A") : Colors.Transparent;
        public Color InProgressTextColor => StatusFilter == "Progress" ? Colors.White : Color.FromArgb("#98a4b3");
        public Color CompletedTextColor => StatusFilter == "Completed" ? Colors.White : Color.FromArgb("#98a4b3");

        public ObservableCollection<Movie> MoviesInProgress { get; set; } = new();
        public ObservableCollection<Movie> UpcomingMovies { get; set; } = new();
        public ObservableCollection<string> Genres { get; set; } = new();

        private string statusFilter = "Progress";
        public string StatusFilter
        {
            get => statusFilter;
            set
            {
                if (statusFilter != value)
                {
                    statusFilter = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(InProgressButtonColor));
                    OnPropertyChanged(nameof(CompletedButtonColor));
                    OnPropertyChanged(nameof(InProgressTextColor));
                    OnPropertyChanged(nameof(CompletedTextColor));
                    FilterMovies();
                }
            }
        }

        public ObservableCollection<string> SortOptions { get; } = new() { "Title", "Rating" };

        private string selectedSortOption;
        public string SelectedSortOption
        {
            get => selectedSortOption;
            set
            {
                if (selectedSortOption != value)
                {
                    selectedSortOption = value;
                    OnPropertyChanged();
                    FilterMovies(); // Re-filter and sort when changed
                }
            }
        }

        public ICommand ShowCompletedCommand { get; }
        public ICommand ShowInProgressCommand { get; }
        public ICommand SelectGenreCommand { get; }
        public ICommand ShowMovieDetailsCommand { get; }

        private bool isGenrePickerVisible;
        public bool IsGenrePickerVisible
        {
            get => isGenrePickerVisible;
            set { isGenrePickerVisible = value; OnPropertyChanged(); }
        }

        private string selectedGenre;
        public string SelectedGenre
        {
            get => selectedGenre;
            set
            {
                if (selectedGenre != value)
                {
                    selectedGenre = value;
                    OnPropertyChanged();
                    FilterMovies();
                }
            }
        }

        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged();
                    FilterMovies();
                }
            }
        }

        private ObservableCollection<Movie> filteredMovies = new();
        public ObservableCollection<Movie> FilteredMovies
        {
            get => filteredMovies;
            set
            {
                filteredMovies = value;
                OnPropertyChanged();
            }
        }

        // Add these properties to MovieViewModel if you want to edit the selected movie's season/episode/part
        private Movie selectedMovie;
        public Movie SelectedMovie
        {
            get => selectedMovie;
            set
            {
                if (selectedMovie != value)
                {
                    selectedMovie = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Season));
                    OnPropertyChanged(nameof(Episode));
                    OnPropertyChanged(nameof(Part));
                }
            }
        }

        public int Season
        {
            get => SelectedMovie?.Season ?? 0;
            set
            {
                if (SelectedMovie != null && SelectedMovie.Season != value)
                {
                    SelectedMovie.UpdateSeason(value);
                    OnPropertyChanged();
                }
            }
        }

        public int Episode
        {
            get => SelectedMovie?.Episode ?? 0;
            set
            {
                if (SelectedMovie != null && SelectedMovie.Episode != value)
                {
                    SelectedMovie.UpdateEpisode(value);
                    OnPropertyChanged();
                }
            }
        }

        public int Part
        {
            get => SelectedMovie?.Part ?? 0;
            set
            {
                if (SelectedMovie != null && SelectedMovie.Part != value)
                {
                    SelectedMovie.UpdatePart(value);
                    OnPropertyChanged();
                }
            }
        }

        public MovieViewModel()
        {
            MoviesInProgress = new ObservableCollection<Movie>
    {
        new Movie("Inception", "placeholder_movie_poster.jpg",
            "Dom Cobb is a skilled thief, the absolute best in the dangerous art of extraction: stealing valuable secrets from deep within the subconscious during the dream state. His rare ability has made him a coveted player in industrial espionage but has also made him a fugitive and cost him everything he loves. Cobb is offered a chance at redemption when he is tasked not with stealing an idea but planting one. If he succeeds, it could be the perfect crime.",
            "Sci-Fi", "Completed", true),

        new Movie("The Dark Knight", "placeholder_movie_poster.jpg",
            "With the help of allies Lt. Jim Gordon and DA Harvey Dent, Batman has been keeping a tight lid on crime in Gotham. But a new criminal mastermind known as the Joker emerges, throwing the city into chaos and pushing Batman closer to crossing the fine line between hero and vigilante. The film explores the limits of justice, the psychology of chaos, and the true cost of heroism.",
            "Action", "Completed", true),

        new Movie("Breaking Bad", "placeholder_movie_poster.jpg",
            "Walter White, a high school chemistry teacher, is diagnosed with terminal lung cancer. In a desperate bid to secure his family's financial future, he partners with former student Jesse Pinkman to manufacture and sell methamphetamine. As the drug empire grows, so does Walter's descent into darkness, transforming him into the feared and ruthless alter ego, 'Heisenberg'.",
            "Drama", "Completed", true),

        new Movie("Stranger Things", "placeholder_movie_poster.jpg",
            "Set in the 1980s, Stranger Things follows a group of young friends in a small town who encounter terrifying supernatural forces and secret government experiments while searching for their missing friend. Along the way, they befriend a mysterious girl with psychokinetic powers, uncover dark alternate dimensions, and battle creatures beyond comprehension.",
            "Horror", "Progress", false),

        new Movie("The Matrix", "placeholder_movie_poster.jpg",
            "Thomas Anderson, known in the hacking world as Neo, discovers that reality as he knows it is a simulated construct created by sentient machines to subdue humanity. With the guidance of Morpheus and Trinity, Neo must awaken to his true potential, challenge the illusion, and fight for the liberation of the human race in a mind-bending battle between freedom and control.",
            "Sci-Fi", "Progress", false),

        new Movie("Game of Thrones", "placeholder_movie_poster.jpg",
            "In the mythical land of Westeros, noble families vie for power and control of the Iron Throne. Alliances are made and broken as political intrigue, betrayal, and war unfold. Amidst the power struggles, an ancient threat from the North reawakens, threatening all of humanity. Game of Thrones is an epic saga of ambition, loyalty, and the brutal realities of power.",
            "Fantasy", "Completed", true),

        new Movie("The Mandalorian", "placeholder_movie_poster.jpg",
            "Set after the fall of the Empire and before the rise of the First Order, this space western follows a lone Mandalorian bounty hunter navigating the lawless galaxy's outer reaches. His mission takes a turn when he encounters a mysterious Force-sensitive child, 'The Child' (Grogu), leading him into conflict with remnants of the Imperial regime and deeper into the Star Wars mythology.",
            "Sci-Fi", "Progress", false),

        new Movie("Money Heist", "placeholder_movie_poster.jpg",
            "Led by the enigmatic Professor, a team of eight skilled criminals carries out an ambitious plan to rob the Royal Mint of Spain and print billions of euros. As hostages are held and authorities scramble, the crew must battle internal tensions, law enforcement, and their own identities in a high-stakes psychological drama filled with unexpected twists.",
            "Thriller", "Progress", false),

        new Movie("The Witcher", "placeholder_movie_poster.jpg",
            "Geralt of Rivia, a solitary monster hunter with enhanced abilities, struggles to find his place in a world where people are often more wicked than beasts. Bound by destiny to the powerful sorceress Yennefer and a young princess with a dangerous secret, Geralt's journey is one of survival, fate, and fighting against the ever-blurring lines of good and evil.",
            "Fantasy", "Completed", true),

        new Movie("Friends", "placeholder_movie_poster.jpg",
            "This beloved sitcom chronicles the personal and professional lives of six friends living in New York City. Through laughter, heartbreak, and life’s ups and downs, Rachel, Ross, Monica, Chandler, Joey, and Phoebe create a bond that feels more like family. Their quirky personalities and memorable moments have made the show a timeless celebration of friendship.",
            "Comedy", "Progress", false),

        // ... Add similar expanded descriptions for the rest of the movies ...
    };

            SelectGenreCommand = new Command<string>(OnGenreSelected);
            ShowCompletedCommand = new Command(() => StatusFilter = "Completed");
            ShowInProgressCommand = new Command(() => StatusFilter = "Progress");
            ShowMovieDetailsCommand = new Command<Movie>(OnShowMovieDetails);

            FilteredMovies = new ObservableCollection<Movie>(MoviesInProgress);

            Genres = new ObservableCollection<string>(
                MoviesInProgress.Select(m => m.Genre).Distinct().OrderBy(g => g)
            )
    {
        "All"
    };

            SelectedGenre = "All";
            FilterMovies();
        }


        private void OnGenreSelected(string genre)
        {
            Debug.WriteLine($"Genre selected: {genre}");
            SelectedGenre = genre;
        }

        private void FilterMovies()
        {
            var filtered = MoviesInProgress.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(m => m.Title.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(SelectedGenre) && SelectedGenre != "All")
                filtered = filtered.Where(m => m.Genre == SelectedGenre);

            if (!string.IsNullOrWhiteSpace(StatusFilter) && StatusFilter != "All")
                filtered = filtered.Where(m => m.Category == StatusFilter);

            FilteredMovies.Clear();
            foreach (var movie in filtered)
                FilteredMovies.Add(movie);
        }

        private string newMovieTitle = string.Empty;
        public string NewMovieTitle
        {
            get => newMovieTitle;
            set { newMovieTitle = value; OnPropertyChanged(); }
        }

        private string newMovieDescription = string.Empty;
        public string NewMovieDescription
        {
            get => newMovieDescription;
            set { newMovieDescription = value; OnPropertyChanged(); }
        }

        private string newMovieCategory = "Progress";
        public string NewMovieCategory
        {
            get => newMovieCategory;
            set { newMovieCategory = value; OnPropertyChanged(); }
        }

        public void AddMovie()
        {
            if (!string.IsNullOrWhiteSpace(NewMovieTitle))
            {
                MoviesInProgress.Add(new Movie(
                    NewMovieTitle,
                    string.Empty,
                    NewMovieDescription,
                    string.Empty,
                    NewMovieCategory,
                    false
                ));
                NewMovieTitle = string.Empty;
                NewMovieDescription = string.Empty;
                FilterMovies();
            }
        }

        private async void OnShowMovieDetails(Movie selectedMovie)
        {
            if (selectedMovie == null)
                return;

            await Shell.Current.Navigation.PushAsync(new BingeBuddy.Pages.MovieDetailsPage(selectedMovie));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
