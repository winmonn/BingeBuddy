namespace BingeBuddy.Models
{
    public class Movie
    {
        public string Title { get; set; } = string.Empty; // Default value
        public string Description { get; set; } = string.Empty; // Default value
        public string Category { get; set; } = "Progress"; // Default value
        public bool Watched { get; set; } = false; // Default value
        public string Thumbnail { get; set; } = string.Empty; // Default value
        public string Genre { get; set; } = string.Empty; // Default value
    }
}
