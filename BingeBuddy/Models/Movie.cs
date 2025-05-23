using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BingeBuddy.Models
{
    public class Movie : INotifyPropertyChanged
    {
        private int _season;
        private int _episode;
        private int _part;
        private bool _inList;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "Progress";
        public bool Watched { get; set; } = false;
        public string CoverPhoto { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;

        private int rating;
        public int Rating
        {
            get => rating;
            set
            {
                if (rating != value)
                {
                    rating = value;
                    OnPropertyChanged();
                }
            }
        }
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

        public bool InList
        {
            get => _inList;
            set
            {
                if (_inList != value)
                {
                    _inList = value;
                    OnPropertyChanged(nameof(InList));
                }
            }
        }

        // Method to update rating and notify change
        public void UpdateRating(int newRating)
        {
            if (Rating != newRating)
            {
                Rating = newRating;
            }
        }
        public void UpdateSeason(int newseason)
        {
            if (Season != newseason)
            {
                Season = newseason;
            }
        }
        public void UpdateEpisode(int newepisode)
        {
            if (Episode != newepisode)
            {
                Episode = newepisode;
            }
        }
        public void UpdatePart(int newpart)
        {
            if (Part != newpart)
            {
                Part = newpart;
            }
        }


        public Movie(string title, string coverPhoto, string description = "", string genre = "", string category = "Progress", bool watched = false, int rating = 0, int season=0,int episode=0, int part=0, bool inList = false)
        {
            Title = title;
            CoverPhoto = coverPhoto;
            Description = description;
            Genre = genre;
            Category = category;
            Watched = watched;
            Rating = rating;
            Season = season;
            Episode = episode;
            Part = part;
            InList = inList;
        }

        public override string ToString()
        {
            return $"{Title} - {Genre}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
