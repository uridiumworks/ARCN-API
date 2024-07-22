using Microsoft.AspNetCore.Http;

namespace ARCN.Application.DataModels.CloudUpload
{

    public class CloudUploadRequestDataModel
    {
        public string? FolderName { get; set; }
        public IFormFile? Files { get; set; }
        public string? ImgName { get; set; }
    }
}
