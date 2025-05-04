using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BingeBuddy.Models;

namespace BingeBuddy.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Movie> MoviesInProgress { get; set; } = new();
        public ObservableCollection<Movie> UpcomingMovies { get; set; } = new();

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
                MoviesInProgress.Add(new Movie
                {
                    Title = NewMovieTitle,
                    Description = NewMovieDescription,
                    Category = NewMovieCategory,
                });
                NewMovieTitle = string.Empty;
                NewMovieDescription = string.Empty;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
