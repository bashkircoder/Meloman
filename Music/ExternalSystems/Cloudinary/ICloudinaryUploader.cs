using Music.ExternalSystems.Cloudinary;
using Music.ExternalSystems.Cloudinary;

namespace Music.ExternalSystems.Cloudinary;

public interface ICloudinaryUploader
{
    Task<UploadedFile> UploadFileAsync(IFormFile? file);
}