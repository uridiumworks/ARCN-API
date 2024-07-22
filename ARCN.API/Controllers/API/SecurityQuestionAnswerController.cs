using ARCN.API.Extension;
using ARCN.Application.DataModels.Security2fa;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ARCN.API.Controllers.API
{
    /// <summary>
    /// Security Question Answer Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityQuestionAnswerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<SecurityAnsweDataModel> validatorSecurityQuestion;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserIdentityService userIdentityService;
        private readonly ISecurityQuestionAnswerRepository securityQuestionAnswerRepository;
        private readonly IEmailService emailService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="validatorSecurityQuestion"></param>
        /// <param name="userManager"></param>
        /// <param name="userIdentityService"></param>
        /// <param name="securityQuestionAnswerRepository"></param>
        /// <param name="emailService"></param>
        public SecurityQuestionAnswerController(IUnitOfWork unitOfWork, IValidator<SecurityAnsweDataModel> validatorSecurityQuestion, UserManager<ApplicationUser> userManager,
            IUserIdentityService userIdentityService,
            ISecurityQuestionAnswerRepository securityQuestionAnswerRepository,
             IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.validatorSecurityQuestion = validatorSecurityQuestion;
            this.userManager = userManager;
            this.userIdentityService = userIdentityService;
            this.securityQuestionAnswerRepository = securityQuestionAnswerRepository;
            this.emailService = emailService;
        }


        /// <summary>
        /// Add Security Question Answer
        /// </summary>
        /// <param name="securityQuestionAnswerList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<ActionResult> Post([FromBody] SecurityQuestionAnswerList securityQuestionAnswerList, CancellationToken cancellationToken)
        {
            if (securityQuestionAnswerList != null)
            {
                var userId = User.GetSubjectId();
                var user = await userManager.FindByIdAsync(userId);
                foreach (var seurityQuestionAnswerModel in securityQuestionAnswerList.SecurityQuestionAnswerDataModels)
                {
                    var validatorResult = await validatorSecurityQuestion.ValidateAsync(seurityQuestionAnswerModel);

                    if (!validatorResult.IsValid)
                    {
                        validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
                        return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
                    }
                    var securityQuestionAns = new SecurityQuestionAnswer();
                    var ans = seurityQuestionAnswerModel.Answer.ToUpper();

                    string hashAns = userManager.PasswordHasher.HashPassword(user, ans);

                    securityQuestionAns.ApplicationUserId = userIdentityService.UserProfileId;
                    securityQuestionAns.SecurityQuestionId = seurityQuestionAnswerModel.QuestionId;
                    securityQuestionAns.Answer = hashAns;

                    var result = await securityQuestionAnswerRepository.AddAsync(securityQuestionAns, cancellationToken);

                }
                await unitOfWork.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
    }
}
