using Music.Models;

namespace Music.ViewModels;

public class AdminUpdateArtist
{
    public Artist Artist { get; set; }
    
    public IFormFile? File { get; set; }
}