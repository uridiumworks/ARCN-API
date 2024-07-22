
using NovaBank.Application.Interfaces;

namespace NovaBank.API.Controllers.Customer.API
{
    /// <summary>
    /// Notification Controller
    /// </summary>
    [Route("customer/api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IValidator<NotificationDataModel> validatorNotification;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserIdentityService userIdentityService;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationRepository"></param>
        /// <param name="validatorNotification"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="userIdentityService"></param>
        public NotificationController(INotificationRepository notificationRepository, IValidator<NotificationDataModel> validatorNotification,
            IMapper mapper, IUnitOfWork unitOfWork, IUserIdentityService userIdentityService)
        {
            this.notificationRepository = notificationRepository;
            this.validatorNotification = validatorNotification;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userIdentityService = userIdentityService;
        }


        /// <summary>
        /// Add Notification
        /// </summary>
        /// <param name="notificationDataModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", Type = typeof(Notification))]
        public async ValueTask<ActionResult<Notification>> Post([FromBody] NotificationDataModel notificationDataModel, CancellationToken cancellationToken)
        {
            var validatorResult = await validatorNotification.ValidateAsync(notificationDataModel);

            if (!validatorResult.IsValid)
            {
                validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            var notifyData = mapper.Map<Notification>(notificationDataModel);

            notifyData.ApplicationUserId = userIdentityService.UserProfileId;

            var result = await notificationRepository.AddAsync(notifyData, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            return Ok(result);
        }


        /// <summary>
        /// Update Notification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notificationDataModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", Type = typeof(Notification))]
        public async ValueTask<ActionResult<Notification>> Put(int id, [FromBody] NotificationDataModel notificationDataModel)
        {

            var validatorResult = await validatorNotification.ValidateAsync(notificationDataModel);

            if (!validatorResult.IsValid)
            {
                validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            var notification = await notificationRepository.FindAll()
                .Where(x => x.NotificationId == id).FirstOrDefaultAsync();

            if (notification is null) return NotFound();

            var notifyData = mapper.Map(notificationDataModel, notification);

            var result = notificationRepository.Update(notifyData);
            await unitOfWork.SaveChangesAsync();

            return Ok(result);
        }

        /// <summary>
        /// Delete Notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json", Type = typeof(Notification))]
        public async ValueTask<ActionResult<Notification>> Delete(int id)
        {
            var notification = await notificationRepository.FindAll()
               .Where(x => x.NotificationId == id).FirstOrDefaultAsync();
            if (notification is null) return NotFound();


            var result = notificationRepository.Remove(notification);
            await unitOfWork.SaveChangesAsync();

            return Ok(result);
        }
    }
}
