namespace Music.Data.Interfaces;

public interface IPhotoRepository
{
    Task<string> UploadPhotoAsync(IFormFile photo);
}