using Music.Models;

namespace Music.ViewModels;

public class HomeViewModel()
{
    public string? ArtistName { get; set; } = "";
    public List<Artist>? Artists { get; set; } = [];
    public int ArtistId { get; set; }
    public bool? IsFavorite { get; set; } = null;
    
    public HashSet<Artist>? FavoriteArtists { get; set; } = [];
    
}