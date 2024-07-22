using NovaBank.API.Filters;
using NovaBank.Application.DataModels.Bill;
using NovaBank.Application.DataModels.RefitDataModels.RingoDataModel;
using NovaBank.Domain.Enums;

namespace NovaBank.API.Controllers.Customer.ODATA
{

    /// <summary>
    /// Bill Controller
    /// </summary>
    [Route("customer/odata")]
    public class BillController : ODataController
    {
        private readonly IBillService _billService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="billService"></param>
        public BillController(IBillService billService)
        {
            _billService = billService;
        }


        /// <summary>
        /// Get Bills By Category
        /// </summary>
        /// <param name="billsType"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/GetBillsByCategory")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ICollection<BillServiceProviderDataModel>>))]

        public async ValueTask<ActionResult> GetBillsByCategory([FromQuery] BillsType? billsType)
        {
            var bills = _billService.GetBillsByCategory(billsType);
            return Ok(bills);
        }

        /// <summary>
        /// Validate Electricity Customer
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/Electricity/ValidateElectricityCustomer/{serviceCode}/{customerNo}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ValidateElectricityCustomerDataModel>))]

        public async ValueTask<ActionResult> ValidateElectricityCustomer([FromRoute] string serviceCode, [FromRoute] string customerNo)
        {
            var bills = _billService.ValidateElectricityCustomer(serviceCode, customerNo);
            return Ok(bills);
        }

        /// <summary>
        /// Get Cable Plans
        /// </summary>
        /// <param name="multichoiceType"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/Cable/Plans")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ICollection<GetCablePlansResponseDataModel>>))]
        public async ValueTask<ActionResult> GetCablePlans([FromQuery] MultichoiceType? multichoiceType)
        {
            var cablePlans = _billService.GetCablePlansAsync(multichoiceType);
            return Ok(cablePlans);
        }

        /// <summary>
        /// Validate Cable Customer
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/Cable/ValidateCableCustomer/{serviceCode}/{customerNo}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ValidateCableCustomerResponseDataModel>))]
        public async ValueTask<ActionResult> ValidateCableCustomer([FromRoute] string serviceCode, [FromRoute] string customerNo)
        {
            var bills = _billService.ValidateCableCustomer(serviceCode, customerNo);
            return Ok(bills);
        }

        /// <summary>
        /// Validate Water Customer
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/Water/ValidateWaterCustomer/{serviceCode}/{customerNo}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ValidateWaterCustomerDataModel>))]
        public async ValueTask<ActionResult> ValidateWaterCustomer([FromRoute] string serviceCode, [FromRoute] string customerNo)
        {
            var bills = _billService.ValidateWaterCustomer(serviceCode, customerNo);
            return Ok(bills);
        }

        /// <summary>
        /// Validate Sport Betting Customer
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/SportBetting/ValidateSportBettingCustomer/{serviceCode}/{customerNo}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ValidateSportBettingCustomerDataModel>))]
        public async ValueTask<ActionResult> ValidateSportBettingCustomer([FromRoute] string serviceCode, [FromRoute] string customerNo)
        {
            var bills = _billService.ValidateSportBettingCustomer(serviceCode, customerNo);
            return Ok(bills);
        }

        /// <summary>
        /// Validate Internet Customer
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/Internet/ValidateInternetCustomer/{serviceCode}/{customerNo}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ValidateInternetCustomerDataModel>))]
        public async ValueTask<ActionResult> ValidateInternetCustomer([FromRoute] string serviceCode, [FromRoute] string customerNo)
        {
            var bills = _billService.ValidateInternetCustomer(serviceCode, customerNo);
            return Ok(bills);
        }


        /// <summary>
        /// Validate Education Customer
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/Education/ValidateEducationCustomer/{serviceCode}/{customerNo}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<ValidateEducationCustomerDataModel>))]
        public async ValueTask<ActionResult> ValidateEducationCustomer([FromRoute] string serviceCode, [FromRoute] string customerNo)
        {
            var bills = _billService.ValidateEducationCustomer(serviceCode, customerNo);
            return Ok(bills);
        }

        /// <summary>
        /// Get Available Data lists
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpGet("Bill/Data/FetchDataList")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(ResponseModel<FetchDataListResponse>))]

        public async ValueTask<ActionResult> FetchDataList([FromQuery] string service)
        {
            var bills = _billService.FetchDataListAsync(service);
            return Ok(bills);
        }

    }
}
