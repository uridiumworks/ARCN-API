using ARCN.Application.DataModels.CloudUpload;

namespace ARCN.Application.Interfaces.Services
{
    public interface ICloudinaryFileUploadService
    {
        ValueTask<CloudUploadResponseDataModel> UploadAsync(CloudUploadRequestDataModel objFile);
        ValueTask<CloudUploadResponseDataModel> UploadDocumentsAsync(CloudUploadRequestDataModel objFile);
        ValueTask<CloudUploadResponseDataModel> UploadBase64ImageAsync(string base64Image, string folderName, string fileName);
    }
}
