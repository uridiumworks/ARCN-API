using ARCN.Application.DataModels.CloudUpload;
using ARCN.Application.Interfaces.Services;

namespace ARCN.Infrastructure.Services.ApplicationServices
{

    public class CloudinaryFileUploadService : ICloudinaryFileUploadService
    {
        private readonly Cloudinary cloudinary;
        private readonly ILogger<CloudinaryFileUploadService> logger;

        public CloudinaryFileUploadService(IOptionsSnapshot<CloudinarySettings> cloudinary,
            ILogger<CloudinaryFileUploadService> logger)
        {

            this.logger = logger;

            var acc = new Account
            (
                cloudinary.Value.CloudName,
                cloudinary.Value.ApiKey,
                cloudinary.Value.ApiSecret
            );
            this.cloudinary = new Cloudinary(acc);
        }
        public async ValueTask<CloudUploadResponseDataModel> UploadAsync(CloudUploadRequestDataModel objFile)
        {
            var response = new CloudUploadResponseDataModel();
            var uploadResult = new ImageUploadResult();
            try
            {
                if (objFile.Files.Length > 0)
                {
                    using var stream = objFile.Files.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(objFile.Files.FileName, stream),
                        //Transformation= new Transformation().Width(100).Height(150).Crop("fill").Gravity("face"),
                        PublicId = objFile.FolderName + "/" + objFile.Files.FileName

                    };
                    uploadResult = await cloudinary.UploadAsync(uploadParams);
                    response.Uri = uploadResult.SecureUri.AbsoluteUri;
                    response.Status = "Successful";
                    response.Name = uploadResult.DisplayName;
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                // response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                return default;
            }
            return response;
        }
        public async ValueTask<CloudUploadResponseDataModel> UploadDocumentsAsync(CloudUploadRequestDataModel objFile)
        {
            var response = new CloudUploadResponseDataModel();
            var uploadResult = new ImageUploadResult();
            try
            {
                if (objFile.Files.Length > 0)
                {
                    using var stream = objFile.Files.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(objFile.ImgName, stream),
                        PublicId = objFile.FolderName + "/" + objFile.ImgName

                    };
                    uploadResult = await cloudinary.UploadAsync(uploadParams);
                    response.Uri = uploadResult.SecureUri.AbsoluteUri;
                    response.Status = "Successful";
                    response.Name = uploadResult.DisplayName;
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                return default;
            }
            return response;
        }

        public async ValueTask<CloudUploadResponseDataModel> UploadBase64ImageAsync(string base64Image, string folderName, string fileName)
        {
            var response = new CloudUploadResponseDataModel();
            var uploadResult = new ImageUploadResult();
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (var ms = new MemoryStream(imageBytes))
                {
                    // Upload parameters
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(fileName, ms),
                        //Transformation= new Transformation().Width(100).Height(150).Crop("fill").Gravity("face"),
                        PublicId = folderName + "/" + fileName

                    };
                    uploadResult = await cloudinary.UploadAsync(uploadParams);
                    response.Uri = uploadResult.SecureUri.AbsoluteUri;
                    response.Status = "Successful";
                    response.Name = uploadResult.DisplayName;
                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
            // Convert base64 string to stream

        }

    }
}
