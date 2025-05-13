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


        public int Season
        {
            get => Movie.Season;
            set { if (Movie.Season != value) { Movie.UpdateSeason(value); OnPropertyChanged(nameof(Season)); } }
        }
        public int Episode
        {
            get => Movie.Episode;
            set { if (Movie.Episode != value) { Movie.UpdateEpisode(value); OnPropertyChanged(nameof(Episode)); } }
        }
        public int Part
        {
            get => Movie.Part;
            set { if (Movie.Part != value) { Movie.UpdatePart(value); OnPropertyChanged(nameof(Part)); } }
        }


        public ICommand RateCommand { get; }
        public ICommand UpdateTrackerCommand { get; }
        public ICommand ToggleInListCommand { get; }

        public MovieDetailsViewModel(Movie movie)
        {
            Movie = movie;
            RateCommand = new Command<object>(OnRate);
            UpdateTrackerCommand = new Command(OnUpdateTracker);
            ToggleInListCommand = new Command(ToggleInList);
            Movie.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Movie.Rating))
                    OnPropertyChanged(nameof(Rating));
            };
        }

        private void ToggleInList()
        {
            if (Movie != null)
            {
                Movie.InList = !Movie.InList;
            }
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
