namespace BingeBuddy.Models
{
    public class Movie
    {
        // Movie Title
        public string Title { get; set; } = string.Empty;

        // Movie Description
        public string Description { get; set; } = string.Empty;

        // Movie Category (e.g., "Progress", "Completed")
        public string Category { get; set; } = "Progress";

        // Whether the movie has been watched
        public bool Watched { get; set; } = false;

        // Thumbnail or Cover Photo Path
        public string CoverPhoto { get; set; } = string.Empty;

        // Movie Genre (e.g., "Action", "Drama")
        public string Genre { get; set; } = string.Empty;

        // Constructor for convenience
        public Movie(string title, string coverPhoto, string description = "", string genre = "", string category = "Progress", bool watched = false)
        {
            Title = title;
            CoverPhoto = coverPhoto;
            Description = description;
            Genre = genre;
            Category = category;
            Watched = watched;
        }

        // Method to Display Movie Details (Optional)
        public override string ToString()
        {
            return $"{Title} - {Genre}";
        }
    }
}
