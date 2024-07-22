
using ARCN.Application.DataModels.Security2fa;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ARCN.API.Controllers.API
{

    /// <summary>
    /// Security Question Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityQuestionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<SecurityQuestionDataModel> validatorSecurityQuest;
        private readonly IMapper mapper;
        private readonly ISecurityQuestionRepository securityQuestionRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="validatorSecurityQuest"></param>
        /// <param name="mapper"></param>
        /// <param name="securityQuestionRepository"></param>
        public SecurityQuestionController(IUnitOfWork unitOfWork, IValidator<SecurityQuestionDataModel> validatorSecurityQuest, IMapper mapper, ISecurityQuestionRepository securityQuestionRepository)
        {
            this.unitOfWork = unitOfWork;
            this.validatorSecurityQuest = validatorSecurityQuest;
            this.mapper = mapper;
            this.securityQuestionRepository = securityQuestionRepository;
        }


        /// <summary>
        /// Add Security Question
        /// </summary>
        /// <param name="question"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", Type = typeof(SecurityQuestion))]
        public async ValueTask<ActionResult<SecurityQuestion>> Post([FromBody] SecurityQuestionDataModel question, CancellationToken cancellationToken)
        {
            var validatorResult = await validatorSecurityQuest.ValidateAsync(question);

            if (!validatorResult.IsValid)
            {
                validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }
            var securityQuestion = mapper.Map<SecurityQuestion>(question);
            var quest = await securityQuestionRepository.AddAsync(securityQuestion, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            return Ok(quest);


        }

        /// <summary>
        /// update Security Question
        /// </summary>
        /// <param name="key"></param>
        /// <param name="question"></param>
        /// <returns></returns>

        [HttpPut("{key}")]
        public async ValueTask<ActionResult> Put(int key, [FromBody] SecurityQuestion question)
        {
            var questionData = await securityQuestionRepository.FindByIdAsync(key);
            if (questionData is null)
                return NotFound();

            questionData.Question = question.Question;

            securityQuestionRepository.Update(questionData);
            await unitOfWork.SaveChangesAsync();

            return Ok();
        }


        /// <summary>
        /// Get All Security Question Based Categories
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetQuestionBasedOnCategory")]
        [Produces("application/json", Type = typeof(SecurityQuestionBasedOnCategoryDataModel))]
        public async ValueTask<ActionResult> GetQuestionBasedOnCategory()
        {
            var securityQuestions = await securityQuestionRepository.GetQuestionsByCategory();
            return Ok(securityQuestions);
        }

    }
}
