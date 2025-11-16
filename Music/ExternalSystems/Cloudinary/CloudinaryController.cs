using Microsoft.AspNetCore.Mvc;
using Music.ExternalSystems.Cloudinary;

namespace Music.ExternalSystems.Cloudinary
{
    public class CloudinaryController : Controller
    {
        private readonly ICloudinaryUploader _cloudinaryUploader;

        public CloudinaryController(ICloudinaryUploader cloudinaryUploader)
        {
            _cloudinaryUploader = cloudinaryUploader;
        }

        [HttpPost]
        public async Task<IActionResult> LoadArrayFiles(List<IFormFile>? files)
        {
            if (files == null || !files.Any())
                return BadRequest("Файлы не выбраны");

            var uploadedFiles = new List<UploadedFile>();

            foreach (var file in files)
            {
                try
                {
                    //var audio = new FormFileAdapter(file);
                    //var uploadedUrl = await _cloudinaryUploader.UploadFileAsync(audio);
                    //uploadedFiles.Add(uploadedUrl);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Ошибка загрузки {file.FileName}: {ex.Message}");
                }
            }

            return Ok(uploadedFiles);
        }

        [HttpPost]
        public async Task<IActionResult> LoadSingleFile(IFormFile? file)
        {
            if (file == null)
                return BadRequest("Файл не выбран");

            try
            {
                //var photo = new FormFileAdapter(file);
                //var uploadedUrl = await _cloudinaryUploader.UploadFileAsync(photo);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка загрузки {file.FileName}: {ex.Message}");
            }
        }
    }
}
