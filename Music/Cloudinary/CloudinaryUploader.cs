using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Music.ExternalSystems.Cloudinary;
using Music.ExternalSystems.Cloudinary;

namespace Music.Cloudinary
{
    public class CloudinaryUploader : ICloudinaryUploader
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryUploader(CloudinaryDotNet.Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<UploadedFile> UploadFileAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Файл не выбран");

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            var isImage = extension is ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".webp";
            var isVideo = extension is ".mp4" or ".mov" or ".avi" or ".webm" or ".mkv";
            var isAudio = extension is ".mp3" or ".wav" or ".ogg" or ".flac" or ".aac" or ".m4a";

            if (!isImage && !isVideo && !isAudio)
                throw new NotSupportedException("Неподдерживаемый тип файла");

            try
            {
                await using var stream = file.OpenReadStream();
                var fileDesc = new FileDescription(file.FileName, stream);
                var publicId = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}";

                RawUploadResult result;

                if (isImage)
                {
                    result = await _cloudinary.UploadAsync(new ImageUploadParams
                    {
                        File = fileDesc,
                        PublicId = publicId
                    });
                }
                else if (isVideo || isAudio)
                {
                    result = await _cloudinary.UploadAsync(new VideoUploadParams
                    {
                        File = fileDesc,
                        PublicId = publicId
                    });
                }
                else
                {
                    throw new NotSupportedException("Невозможно определить тип файла");
                }

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception($"Ошибка загрузки файла в Cloudinary: {result.Error?.Message}");

                return new UploadedFile
                {
                    FileName = result.OriginalFilename,
                    Url = result.SecureUrl?.ToString() ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка загрузки: {ex.Message}", ex);
            }
        }
    }
}
