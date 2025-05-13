using System.ComponentModel;
using System.Windows.Input;
using BingeBuddy.Models;

namespace BingeBuddy.ViewModels
{
    public class MovieDetailsViewModel : INotifyPropertyChanged
    {
        public Movie Movie { get; }

        public string Title => Movie.Title;
        public string Description => Movie.Description;
        public string CoverPhoto => Movie.CoverPhoto;

        public int Rating
        {
            get => Movie.Rating;
            set
            {
                if (Movie.Rating != value)
                {
                    Movie.UpdateRating(value);
                    // No need to call OnPropertyChanged(nameof(Rating)) here,
                    // it will be triggered by the Movie.PropertyChanged event below.
                }
            }
        }

        public ICommand RateCommand { get; }

        public MovieDetailsViewModel(Movie movie)
        {
            Movie = movie;
            RateCommand = new Command<object>(OnRate);

            Movie.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Movie.Rating))
                    OnPropertyChanged(nameof(Rating));
            };
        }

        private void OnRate(object value)
        {
            if (value is int intValue)
                Rating = intValue;
            else if (value is string str && int.TryParse(str, out int parsed))
                Rating = parsed;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
