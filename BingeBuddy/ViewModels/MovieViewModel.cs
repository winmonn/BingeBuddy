using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BingeBuddy.Models;

namespace BingeBuddy.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Movie> Movies { get; set; } = new();
        private string newMovieTitle;
        public string NewMovieTitle
        {
            get => newMovieTitle;
            set { newMovieTitle = value; OnPropertyChanged(); }
        }

        public void AddMovie()
        {
            if (!string.IsNullOrWhiteSpace(NewMovieTitle))
            {
                Movies.Add(new Movie { Title = NewMovieTitle, Watched = false });
                NewMovieTitle = string.Empty;
            }
        }

        public void RemoveMovie(Movie movie) => Movies.Remove(movie);
        public void ToggleWatched(Movie movie) => movie.Watched = !movie.Watched;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
