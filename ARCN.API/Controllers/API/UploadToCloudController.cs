using ARCN.Application.DataModels.CloudUpload;
using ARCN.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace ARCN.API.Controllers.API
{

    /// <summary>
    /// Upload To Cloud Controller
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UploadToCloudController : ControllerBase
    {
        private readonly ICloudinaryFileUploadService fileUploadService;
        private readonly ILogger<UploadToCloudController> logger;
        private readonly IOptions<List<FolderDataModel>> folderNames;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileUploadService"></param>
        /// <param name="logger"></param>
        /// <param name="folderNames"></param>
        public UploadToCloudController(ICloudinaryFileUploadService fileUploadService,
             ILogger<UploadToCloudController> logger,
              IOptions<List<FolderDataModel>> folderNames)
        {
            this.fileUploadService = fileUploadService;
            this.logger = logger;
            this.folderNames = folderNames;
        }


        /// <summary>
        /// Upload To Cloud
        /// </summary>
        /// <param name="objFile"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("FileUpload")]
        [Produces("application/json", Type = typeof(string))]

        public async Task<ActionResult<string>> UploadToCloud([FromForm] CloudUploadRequestDataModel objFile)
        {
            var folder = folderNames.Value.Where(x => x.Name == objFile.FolderName);
            if (!folder.Any())
            {
                ModelState.AddModelError("UploadToCloud", "Folder Name Not Valid");
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }
            var result = await fileUploadService.UploadAsync(objFile);
            if (result.Status != "Successful")
            {

                ModelState.AddModelError("UploadToCloud", result.Status);
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            logger.LogInformation("FileUploaded successfully with new image url -  {0}", result.Uri);

            return Ok(result.Uri);
        }


        /// <summary>
        /// Document Upload To Cloud
        /// </summary>
        /// <param name="objFile"></param>
        /// <returns></returns>
        [HttpPost("DocumentUpload")]
        [Produces("application/json", Type = typeof(string))]
        public async Task<ActionResult<string>> UploadDocumentToCloud([FromForm] CloudUploadRequestDataModel objFile)
        {
            var folder = folderNames.Value.Where(x => x.Name == objFile.FolderName);
            if (!folder.Any())
            {
                ModelState.AddModelError("UploadToCloud", "Folder Name Not Valid");
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }
            var result = await fileUploadService.UploadDocumentsAsync(objFile);
            if (result.Status != "Successful")
            {

                ModelState.AddModelError("UploadToCloud", result.Status);
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            logger.LogInformation("FileUploaded successfully with new image url -  {0}", result.Uri);

            return Ok(result.Uri);
        }

    }
}
