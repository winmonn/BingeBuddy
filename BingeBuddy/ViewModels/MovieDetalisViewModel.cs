using System.ComponentModel;
using System.Windows.Input;
using BingeBuddy.Models;

namespace BingeBuddy.ViewModels
{
    public class MovieDetailsViewModel : INotifyPropertyChanged
    {
        public Movie Movie { get; }

        public string Title => Movie.Title;
        public string Genre => Movie.Genre;
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
                }
            }
        }

        // Tracker for current season, episode, and part
        private int _season;
        public int Season
        {
            get => _season;
            set
            {
                if (_season != value)
                {
                    _season = value;
                    OnPropertyChanged(nameof(Season));
                }
            }
        }

        private int _episode;
        public int Episode
        {
            get => _episode;
            set
            {
                if (_episode != value)
                {
                    _episode = value;
                    OnPropertyChanged(nameof(Episode));
                }
            }
        }

        private int _part;
        public int Part
        {
            get => _part;
            set
            {
                if (_part != value)
                {
                    _part = value;
                    OnPropertyChanged(nameof(Part));
                }
            }
        }

        public ICommand RateCommand { get; }
        public ICommand UpdateTrackerCommand { get; }

        public MovieDetailsViewModel(Movie movie)
        {
            Movie = movie;
            RateCommand = new Command<object>(OnRate);
            UpdateTrackerCommand = new Command(OnUpdateTracker);

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

        // Method to update season, episode, and part (could be called on button press or other UI interactions)
        private void OnUpdateTracker()
        {
            // Logic for updating tracker could go here, such as saving it to a database or local storage.
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
