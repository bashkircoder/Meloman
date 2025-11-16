using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Common;
using Music.Data.Interfaces;
using Music.Data.Repositories;
using Music.Data.Interfaces;
using Music.Filters;
using Music.Logger.Interfaces;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

public class HomeController(IArtistRepository artistRepository, IUserRepository userRepository, IMusicLogger musicLogger) : Controller
{
    
    private readonly string _controller = ControllerHelper.GetControllerName<HomeController>();
    
    public async Task<IActionResult> Index(string artistName = "")
    {
        
        var favoriteArtists = await userRepository.GetFavoriteArtistsAsync();
        
        var artists = await artistRepository.GetAllAsync();
      
        
        if (artistName != "")
        {
            var filteredArtists = artists.Where(a => a.Name.Contains(artistName, StringComparison.InvariantCultureIgnoreCase)).ToList();

            var viewModel = new HomeViewModel 
            {
                Artists = filteredArtists,
                ArtistName = artistName,
                FavoriteArtists = favoriteArtists
            };
            
            return View(viewModel);
        }
        else
        {
            var viewModel = new HomeViewModel
            {
                Artists = artists,
                ArtistName = artistName,
                FavoriteArtists = favoriteArtists
            };
        
            return View(viewModel);
        }
    }
    
    public async Task<IActionResult> Favorites(bool isFavorite, int artistId, string inputName = "")
    {
        
        if (isFavorite == true)
        {
            await AddArtistToFavorites(artistId);
        }
        else
        {
            await RemoveArtistFromFavorites(artistId);
        }
      
        return RedirectToAction(nameof(HomeController.Index), _controller, new {artistName = inputName});
    }
    public async Task AddArtistToFavorites(int artistId)
    {
        var artist = await artistRepository.GetDetailsByIdAsync(artistId);
        await userRepository.AddFavoriteArtist(artist);
        
    }
    
    public async Task RemoveArtistFromFavorites(int artistId)
    {
        var artist = await artistRepository.GetDetailsByIdAsync(artistId);
        await userRepository.RemoveFavoriteArtist(artist);
        
    }
}