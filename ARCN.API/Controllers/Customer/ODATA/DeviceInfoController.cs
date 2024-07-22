namespace NovaBank.API.Controllers.Customer.ODATA
{
    /// <summary>
    /// Device Info Controller
    /// </summary>
    [Route("customer/odata")]
    public class DeviceInfoController : ODataController
    {
        private readonly IDeviceInfoRepository deviceInfoRepository;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="deviceInfoRepository"></param>
        public DeviceInfoController(IDeviceInfoRepository deviceInfoRepository)
        {
            this.deviceInfoRepository = deviceInfoRepository;
        }


        /// <summary>
        /// Get All Device Info
        /// </summary>
        /// <returns></returns>
        [HttpGet("DeviceInfo")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(List<DeviceInfo>))]

        public ActionResult GetAll()
        {
            var deviceInfos = deviceInfoRepository.FindAll();
            return Ok(deviceInfos);
        }


        /// <summary>
        /// Get One Device Info
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("DeviceInfo/{key}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(SingleResult<DeviceInfo>))]
        public ActionResult GetOne(int key)
        {
            var deviceInfo = deviceInfoRepository
                .Get(x => x.DeviceInfoId == key);

            return Ok(SingleResult.Create(deviceInfo));
        }
    }
}
