using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Interfaces;
using Music.Data.Repositories;
using Music.Data.Interfaces;
using Music.Exceptions;
using Music.Filters;

namespace Music.Controllers;

[ExceptionFilter]
public class SongController(IAlbumRepository albumRepository) : Controller
{
    public async Task<IActionResult> Index(int id)
    {
        var album = await albumRepository.GetDetailsByIdAsync(id);
                            
        return View(album);
    }
}