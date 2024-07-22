using iTextSharp.text;
using iTextSharp.text.pdf;
using NovaBank.API.Filters;

namespace NovaBank.API.Controllers.Customer.ODATA
{
    /// <summary>
    /// Transaction History Controller
    /// </summary>
    [Route("customer/odata")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase //ODataController
    {
        private readonly ITransactionHistoryRepository transactionHistoryRepository;
        private readonly ITransactionService transactionService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transactionHistoryRepository"></param>
        /// <param name="transactionService"></param>
        public TransactionHistoryController(ITransactionHistoryRepository transactionHistoryRepository, ITransactionService transactionService)
        {
            this.transactionHistoryRepository = transactionHistoryRepository;
            this.transactionService = transactionService;
        }

        /// <summary>
        /// Get All Transaction History
        /// </summary>
        /// <returns></returns>
       // [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("TransactionHistory")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(List<TransactionHistory>))]

        public async ValueTask<ActionResult> GetAll()
        {
            var transactionHistories = transactionHistoryRepository.FindAll().Include(x => x.ApplicationUser);//AsNoTracking();
            return Ok(transactionHistories);
        }


        /// <summary>
        /// Get One Transaction History
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("TransactionHistory/{key}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(SingleResult<TransactionHistory>))]
        public ActionResult GetOne(int key)
        {
            var transactionHistory = transactionHistoryRepository
                .Get(x => x.TransactionHistoryId == key).Include(x=>x.ApplicationUser)
                .AsNoTracking();

            return Ok(SingleResult.Create(transactionHistory));
        }

        /// <summary>
        /// Get Account Balance
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("GetBalance")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(decimal))]


        public async Task<ActionResult> GetBalance(string AccountNumber)
        {
            var AcctNumber = this.transactionService.GetAccountBalance(AccountNumber);
            return Ok(AcctNumber);
        }

        /// <summary>
        /// Get Account Balance Profile
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("GetAccountBalanceProfile")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<List<Application.DataModels.BalanceProfile.BalanceProfileDataModel>>))]
        public async Task<ActionResult> GetBalanceProfile()
        {
            var BalanceProfile = this.transactionService.GetBalanceProfile();
            return Ok(BalanceProfile);
        }

        /// <summary>
        /// Get Account Statement
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="period"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("AccountStatement")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<Application.Helper.Pagination.PaginationModel<IEnumerable<Application.DataModels.AccountStatementDataModel.AccountStatementDataModel>>>))]

        public async Task<ActionResult> GetAccountStatement(string accountNumber, string period, DateTime FromDate, DateTime ToDate, int pageSize, int pageNumber)
        {
            var statement = await this.transactionService.GetAccountStatement(accountNumber, period, FromDate, ToDate, pageSize, pageNumber);
            return Ok(statement);
        }

        /// <summary>
        /// Download Statement
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("DownloadStatement")]
        [EnableQuery]
        public async Task<ActionResult> DownloadStatement(string accountNumber, DateTime month)
        {
            var statement = await this.transactionService.DownloadStatement(accountNumber, month);

            Document document = new Document();

            Encoding u8 = Encoding.UTF8;

            byte[] result = statement.Data.SelectMany(x => u8.GetBytes(x.ToString())).ToArray();

            MemoryStream stream = new MemoryStream();

            PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
            pdfWriter.CloseStream = false;
            document.Open();
            foreach (var x in result)
            {
                document.Add(new Paragraph(x));
            }

            document.Close();
            stream.Flush();
            return File(stream, "application/pdf", $"Statement {month.Month.ToString()}");
        }
    }
}
