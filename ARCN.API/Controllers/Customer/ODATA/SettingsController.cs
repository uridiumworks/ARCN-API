namespace NovaBank.API.Controllers.Customer.ODATA
{

    /// <summary>
    /// Settings Controller
    /// </summary>
    [Route("customer/odata")]
    [ApiController]
    public class SettingsController : ODataController
    {
        private readonly ITransferService transferService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transferService"></param>
        public SettingsController(ITransferService transferService)
        {
            this.transferService = transferService;
        }

        /// <summary>
        /// List Transfer Beneficiaries
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("Settings/beneficiaries")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ICollection<Application.DataModels.Transfer.TransferBeneficiaryDataModel>>))]
        public async ValueTask<ActionResult> ListTransferBeneficiaries([FromQuery] string? search)
        {
            var beneficiaries = await transferService.ListTransferBeneficiaries(search);
            return Ok(beneficiaries);
        }
    }
}
