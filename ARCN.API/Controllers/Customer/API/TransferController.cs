using NovaBank.API.Filters;
using NovaBank.Application.DataModels.Transfer;

namespace NovaBank.API.Controllers.Customer.API
{
    /// <summary>
    /// Transfer Controller
    /// </summary>
    [Route("customer/api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transferService"></param>
        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }


        /// <summary>
        /// Initiate Transfer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("initiate")]
        [Produces("application/json", Type = typeof(ResponseModel<InitiateTransferResponseDataModel>))]
        public async ValueTask<ActionResult> InitiateTransfer([FromBody] InitiateTransferRequestDataModel request, CancellationToken cancellationToken)
        {
            var response = await _transferService.InitiateTransfer(request, cancellationToken);

            return Ok(response);
        }

    }
}
