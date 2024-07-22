using ARCN.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.ODATA
{

    /// <summary>
    /// Security Question Controller
    /// </summary>
    [Route("odata")]
    public class SecurityQuestionController : ODataController
    {
        private readonly ISecurityQuestionRepository securityQuestionRepository;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="securityQuestionRepository"></param>
        public SecurityQuestionController(ISecurityQuestionRepository securityQuestionRepository)
        {
            this.securityQuestionRepository = securityQuestionRepository;
        }

        /// <summary>
        /// Get All Security Question
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("SecurityQuestion")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(List<SecurityQuestion>))]
        public ActionResult GetAll()
        {
            var securityQuestions = securityQuestionRepository.FindAll();
            return Ok(securityQuestions);
        }


        /// <summary>
        /// Get One Security Question
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("SecurityQuestion/{key}")]
        [EnableQuery]
        [Produces("application/json", Type = typeof(SingleResult<SecurityQuestion>))]
        public ActionResult GetOne(int key)
        {
            var securityQuestions = securityQuestionRepository
                .Get(x => x.SecurityQuestionId == key);

            return Ok(SingleResult.Create(securityQuestions));
        }
    }
}
