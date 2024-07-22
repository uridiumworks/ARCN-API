namespace NovaBank.API.Controllers.Customer.ODATA
{

    /// <summary>
    /// Notification Controller
    /// </summary>
    [Route("customer/odata")]
    public class NotificationController : ODataController
    {
        private readonly INotificationRepository notificationRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationRepository"></param>
        public NotificationController(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }


        /// <summary>
        /// Get All Notification
        /// </summary>
        /// <returns></returns>
        [HttpGet("Notification")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(IOrderedQueryable<Notification>))]
        public ActionResult GetAll()
        {
            var notifications = notificationRepository.FindAll().OrderByDescending(x => x.CreatedDate);
            return Ok(notifications);
        }


        /// <summary>
        /// Get One Notification
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("Notification/{key}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(SingleResult<Notification>))]
        public ActionResult GetOne(int key)
        {
            var notification = notificationRepository
                .Get(x => x.NotificationId == key);

            return Ok(SingleResult.Create(notification));
        }
    }
}
