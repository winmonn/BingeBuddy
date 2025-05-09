using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using BingeBuddy.Models;

namespace BingeBuddy.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Movie> MoviesInProgress { get; set; } = new();
        public ObservableCollection<Movie> UpcomingMovies { get; set; } = new();

        // Add these for search/filter support
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

        public MovieViewModel()
        {
            MoviesInProgress.Add(new Movie(
                "Inception",
                "placeholder_movie_poster.jpg",
                "A mind-bending thriller about dream invasion.",
                "Sci-Fi",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "The Dark Knight",
                "placeholder_movie_poster.jpg",
                "A superhero battles crime in Gotham City.",
                "Action",
                "Completed",
                true
            ));
            MoviesInProgress.Add(new Movie(
                "Breaking Bad",
                "placeholder_movie_poster.jpg",
                "A high school teacher turned drug kingpin.",
                "Drama",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "Stranger Things",
                "placeholder_movie_poster.jpg",
                "A group of kids uncover supernatural mysteries.",
                "Horror",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "The Matrix",
                "placeholder_movie_poster.jpg",
                "A computer hacker learns about the true nature of reality.",
                "Sci-Fi",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "Game of Thrones",
                "placeholder_movie_poster.jpg",
                "Noble families vie for control of the Iron Throne.",
                "Fantasy",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "The Mandalorian",
                "placeholder_movie_poster.jpg",
                "A lone bounty hunter in the outer reaches of the galaxy.",
                "Sci-Fi",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "Money Heist",
                "placeholder_movie_poster.jpg",
                "A criminal mastermind plans the biggest heist in history.",
                "Thriller",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "The Witcher",
                "placeholder_movie_poster.jpg",
                "A mutated monster-hunter struggles to find his place in a world.",
                "Fantasy",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "Friends",
                "placeholder_movie_poster.jpg",
                "Follows the personal and professional lives of six friends in New York.",
                "Comedy",
                "Progress",
                false
            ));
            MoviesInProgress.Add(new Movie(
                "Sherlock",
                "placeholder_movie_poster.jpg",
                "A modern update finds the famous sleuth and his doctor partner solving crime in 21st century London.",
                "Mystery",
                "Progress",
                false
            ));

            // Initialize FilteredMovies with all movies
            FilteredMovies = new ObservableCollection<Movie>(MoviesInProgress);
        }

        private void FilterMovies()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredMovies = new ObservableCollection<Movie>(MoviesInProgress);
            }
            else
            {
                var filtered = MoviesInProgress
                    .Where(m => m.Title.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
                FilteredMovies = new ObservableCollection<Movie>(filtered);
            }
        }

        private string newMovieTitle = string.Empty; // Default value
        public string NewMovieTitle
        {
            get => newMovieTitle;
            set { newMovieTitle = value; OnPropertyChanged(); }
        }

        private string newMovieDescription = string.Empty; // Default value
        public string NewMovieDescription
        {
            get => newMovieDescription;
            set { newMovieDescription = value; OnPropertyChanged(); }
        }

        private string newMovieCategory = "Progress"; // Default value
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
                    string.Empty, // Default value for CoverPhoto
                    NewMovieDescription,
                    string.Empty, // Default value for Genre
                    NewMovieCategory,
                    false // Default value for Watched
                ));
                NewMovieTitle = string.Empty;
                NewMovieDescription = string.Empty;
                FilterMovies(); // Update filtered list after adding
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
