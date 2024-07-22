namespace NovaBank.API.Controllers.Customer.ODATA
{
    /// <summary>
    /// Geo Location Controller
    /// </summary>
    [Route("customer/odata")]
    public class GeoLocationController : ODataController
    {
        private readonly IGeoLocationRepository geoLocationRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="geoLocationRepository"></param>
        public GeoLocationController(IGeoLocationRepository geoLocationRepository)
        {
            this.geoLocationRepository = geoLocationRepository;
        }

        /// <summary>
        /// Get All Geo Location
        /// </summary>
        /// <returns></returns>
        [HttpGet("GeoLocation")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(List<GeoLocation>))]
        public ActionResult GetAll()
        {
            var geoLocations = geoLocationRepository.FindAll();
            return Ok(geoLocations);
        }

        /// <summary>
        /// Get One Geo Location
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        [HttpGet("GeoLocation/{key}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(SingleResult<GeoLocation>))]
        public ActionResult GetOne(int key)
        {
            var geoLocation = geoLocationRepository
                .Get(x => x.GeoLocationId == key);

            return Ok(SingleResult.Create(geoLocation));
        }
    }
}
