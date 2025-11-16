using Microsoft.AspNetCore.Mvc.Rendering;
using Music.Common;
using Music.Data.Repositories;
using Music.Helpers;
using Music.Models;

namespace Music.ViewModels;

public class AlbumViewModel
{
    public int ArtistId { get; set; }

    public int AlbumId { get; set; }

    public  SortedType SortedType { get; set; } = SortedType.None;

    public string? AlbumName { get; set; } = "";

    public List<Album>? Albums { get; set; } = [];

    public PageViewModel? PageViewModel { get; set; }

    public int PageQuantity { get; set; } = 1;
    public int PageNumber { get; set; } = 1;
    
    public bool? IsFavorite { get; set; } = null;
    
    public HashSet<Album> FavoriteAlbums { get; set; } = [];
}