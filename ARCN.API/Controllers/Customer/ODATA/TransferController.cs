using NovaBank.API.Filters;

namespace NovaBank.API.Controllers.Customer.ODATA
{
    /// <summary>
    /// Transfer Controller
    /// </summary>
    [Route("customer/odata")]
    public class TransferController : ODataController
    {
        private readonly ITransferService transferService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transferService"></param>
        public TransferController(ITransferService transferService)
        {
            this.transferService = transferService;
        }


        /// <summary>
        /// Get Banks
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Transfer/GetBanks")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ICollection<Application.DataModels.Transfer.GetBanksDataModel>>))]
        public async ValueTask<ActionResult> GetBanks([FromQuery] string? search)
        {
            var banks = transferService.GetBanks(search);
            return Ok(banks);
        }


        /// <summary>
        /// Resolve Intra Bank Account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Transfer/{accountNumber}/resolve")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<Application.DataModels.Transfer.ResolveBankAccountDataModel>))]

        public async ValueTask<ActionResult> ResolveIntraBankAccount([FromRoute] string accountNumber)
        {
            var bankAccount = await transferService.ResolveIntraBankAccount(accountNumber);
            return Ok(bankAccount);
        }

        /// <summary>
        /// Resolve Inter Bank Account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Transfer/{accountNumber}/{bankCode}/resolve")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<Application.DataModels.Transfer.ResolveBankAccountDataModel>))]
        public async ValueTask<ActionResult> ResolveInterBankAccount([FromRoute] string accountNumber, [FromRoute] string bankCode)
        {
            var bankAccount = await transferService.ResolveInterBankAccount(accountNumber, bankCode);
            return Ok(bankAccount);
        }

        /// <summary>
        /// List Transfer Beneficiaries
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Transfer/beneficiaries")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ICollection<Application.DataModels.Transfer.TransferBeneficiaryDataModel>>))]
        public async ValueTask<ActionResult> ListTransferBeneficiaries([FromQuery] string? search)
        {
            var beneficiaries = await transferService.ListTransferBeneficiaries(search);
            return Ok(beneficiaries);
        }

        /// <summary>
        /// Get Transfer Beneficiary
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Transfer/details/{reference}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<Application.DataModels.Transfer.TransferBeneficiaryDataModel>))]
        public async ValueTask<ActionResult> GetTransferTransactionDetails([FromRoute] string reference)
        {
            var transferDetails = await transferService.GetTransferTransactionDetails(reference);
            return Ok(transferDetails);
        }

        /// <summary>
        /// Get All Transfer Transactions
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Transfer/transfers")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<Application.Helper.Pagination.PaginationModel<IEnumerable<Application.DataModels.Transfer.TransferTransactionDetailsDataModel>>>))]
        public async ValueTask<ActionResult> GetAllTransferTransactions([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var transfersDetails = await transferService.GetAllTransferTransactions(pageSize, pageNumber);
            return Ok(transfersDetails);
        }
    }
}
