using ARCN.Application.DataModels.Identity;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Commons.Authorization;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Errors = ARCN.Application.Exceptions;

namespace ARCN.API.Controllers.ODATA
{
    [Route("odata")]
    public class RoleController : ODataController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<RoleController> logger;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IValidator<AddUserToRoleDataModel> validatorAddUserToRole;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            ILogger<RoleController> logger, IUserService userService, IMapper mapper, IValidator<AddUserToRoleDataModel> validatorAddUserToRole)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.userService = userService;
            this.mapper = mapper;
            this.validatorAddUserToRole = validatorAddUserToRole;
        }

        [HttpGet("Role")]
        public async ValueTask<ActionResult<List<IdentityRole>>> Get()
        {
            var roles = await roleManager.Roles.ToListAsync();

            return Ok(roles);
        }

        [HttpGet("Role/{key}")]
        public async ValueTask<ActionResult<IdentityRole>> Get(string key)
        {
            var role = await roleManager.Roles.Where(x => x.Id == key).FirstOrDefaultAsync();

            return Ok(role);
        }

        [HttpPost("Role")]
        public async ValueTask<ActionResult<IdentityRole>> Post([FromBody] RoleDataModel role)
        {

            if (string.IsNullOrEmpty(role.Name) || role == null) return BadRequest();

            if (!await roleManager.RoleExistsAsync(role.Name.Trim()))
            {
                var result = await roleManager.CreateAsync(new IdentityRole { Name = role.Name });
                if (result.Succeeded)
                {
                    var roleData = await roleManager.FindByNameAsync(role.Name);

                    await userService.AddPermissionToRole(role.RoleClaims, roleData.Id);
                    return Ok(roleData);
                }
            }

            return BadRequest();

        }

       

        [HttpGet("GetRolesClaims")]
        public async ValueTask<ActionResult<List<RoleClaimResponseDataModel>>> GetRolesClaims()
        {
            var RoleClaims = await userService.GetAllRoleClaims();

            return RoleClaims;


        }

        [HttpGet("GetAllUnAssignedClaims")]
        public async ValueTask<ActionResult<List<RoleClaimDataModel>>> GetAllUnAssignedClaims()
        {
            var RoleClaims = await userService.GetAllUnAssignedClaims();

            return RoleClaims;


        }

        [HttpGet("GetRoleClaims/{roleId}")] // First to be called for update.
        public async ValueTask<ActionResult<RoleClaimResponseDataModel>> GetRoleClaims(string roleId)
        {
            var RoleClaims = await userService.GetPermissionAsync(roleId);

            return RoleClaims;


        }

       
    }
}
