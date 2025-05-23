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
                    OnPropertyChanged(nameof(FilteredMoviesInList));
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
                    OnPropertyChanged(nameof(FilteredMoviesInList));
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
                    OnPropertyChanged(nameof(FilteredMoviesInList));
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
                    OnPropertyChanged(nameof(FilteredMoviesInList));
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

        public ObservableCollection<Movie> FilteredMoviesInList
        {
            get
            {
                IEnumerable<Movie> filtered = MoviesInProgress.Where(m => m.InList);

                if (!string.IsNullOrWhiteSpace(SearchText))
                    filtered = filtered.Where(m => m.Title.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(SelectedGenre) && SelectedGenre != "All")
                    filtered = filtered.Where(m => m.Genre == SelectedGenre);

                if (!string.IsNullOrWhiteSpace(StatusFilter) && StatusFilter != "All")
                    filtered = filtered.Where(m => m.Category == StatusFilter);

                if (SelectedSortOption == "Title")
                    filtered = filtered.OrderBy(m => m.Title);
                else if (SelectedSortOption == "Rating")
                    filtered = filtered.OrderByDescending(m => m.Rating);

                return new ObservableCollection<Movie>(filtered);
            }
        }

        public ObservableCollection<Movie> MoviesInList
        {
            get => new ObservableCollection<Movie>(MoviesInProgress.Where(m => m.InList));
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
                new Movie("Inception", "placeholder_movie_poster.jpg", "A mind-bending thriller about dream invasion.", "Sci-Fi", "Completed", true, 4, 1, 1, 0, true),
                new Movie("The Dark Knight", "placeholder_movie_poster.jpg", "A superhero battles crime in Gotham City.", "Action", "Completed", true, 3, 1, 1, 0, true),
                new Movie("Breaking Bad", "placeholder_movie_poster.jpg", "A high school teacher turned drug kingpin.", "Drama", "Completed", true, 5, 5, 62, 0, true),
                new Movie("Stranger Things", "placeholder_movie_poster.jpg", "A group of kids uncover supernatural mysteries.", "Horror", "Progress", false, 0, 0, 0, 0, false),
                new Movie("The Matrix", "placeholder_movie_poster.jpg", "A computer hacker learns about the true nature of reality.", "Sci-Fi", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Game of Thrones", "placeholder_movie_poster.jpg", "Noble families vie for control of the Iron Throne.", "Fantasy", "Completed", true, 4, 8, 73, 0, true),
                new Movie("The Mandalorian", "placeholder_movie_poster.jpg", "A lone bounty hunter in the outer reaches of the galaxy.", "Sci-Fi", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Money Heist", "placeholder_movie_poster.jpg", "A criminal mastermind plans the biggest heist in history.", "Thriller", "Progress", false, 0, 0, 0, 0, true),
                new Movie("The Witcher", "placeholder_movie_poster.jpg", "A mutated monster-hunter struggles to find his place in a world.", "Fantasy", "Completed", true, 5, 3, 24, 0, true),
                new Movie("Friends", "placeholder_movie_poster.jpg", "Follows the personal and professional lives of six friends in New York.", "Comedy", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Sherlock", "placeholder_movie_poster.jpg", "A modern update finds the famous sleuth and his doctor partner solving crime in 21st century London.", "Mystery", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Avatar: The Last Airbender", "placeholder_movie_poster.jpg", "A young boy must master all four elements to save the world.", "Animation", "Progress", false, 0, 0, 0, 0, false),
                new Movie("House of Cards", "placeholder_movie_poster.jpg", "A ruthless politician will stop at nothing to conquer Washington, D.C.", "Drama", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Lost", "placeholder_movie_poster.jpg", "Survivors of a plane crash struggle to survive on a mysterious island.", "Adventure", "Progress", false, 0, 0, 0, 0, false),
                new Movie("The Office", "placeholder_movie_poster.jpg", "A mockumentary on a group of typical office workers.", "Comedy", "Completed", true, 4, 9, 201, 0, true),
                new Movie("Peaky Blinders", "placeholder_movie_poster.jpg", "A gangster family epic set in 1900s England.", "Crime", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Black Mirror", "placeholder_movie_poster.jpg", "An anthology series exploring a twisted, high-tech world.", "Sci-Fi", "Progress", false , 0, 0, 0, 0, true),
                new Movie("Narcos", "placeholder_movie_poster.jpg", "A chronicled look at the criminal exploits of Colombian drug lord Pablo Escobar.", "Crime", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Vikings", "placeholder_movie_poster.jpg", "The adventures of Ragnar Lothbrok, the greatest hero of his age.", "Action", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Brooklyn Nine-Nine", "placeholder_movie_poster.jpg", "Comedy series following the exploits of a Brooklyn police precinct.", "Comedy", "Progress", false, 0, 0, 0, 0, false),
                new Movie("Chernobyl", "placeholder_movie_poster.jpg", "A dramatization of the true story of the Chernobyl nuclear disaster.", "Drama", "Completed", true, 5, 1, 5, 0, true)
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


        private void FilterMovies(bool onlyInList = false)
        {
            var filtered = MoviesInProgress.AsEnumerable();

            if (onlyInList)
                filtered = filtered.Where(m => m.InList);

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(m => m.Title.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(SelectedGenre) && SelectedGenre != "All")
                filtered = filtered.Where(m => m.Genre == SelectedGenre);

            if (!string.IsNullOrWhiteSpace(StatusFilter) && StatusFilter != "All")
                filtered = filtered.Where(m => m.Category == StatusFilter);

            if (SelectedSortOption == "Title")
                filtered = filtered.OrderBy(m => m.Title);
            else if (SelectedSortOption == "Rating")
                filtered = filtered.OrderByDescending(m => m.Rating);

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
