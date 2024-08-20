using ARCN.Application.DataModels.Identity;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces.Services;
using ARCN.Domain.Commons.Authorization;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Errors = ARCN.Application.Exceptions;

namespace ARCN.API.Controllers.API
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RoleController:ODataController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<RoleController> logger;
        private readonly IUserSettingService userService;
        private readonly IMapper mapper;
        private readonly IValidator<AddUserToRoleDataModel> validatorAddUserToRole;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            ILogger<RoleController> logger, IUserSettingService userService, IMapper mapper, IValidator<AddUserToRoleDataModel> validatorAddUserToRole)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.userService = userService;
            this.mapper = mapper;
            this.validatorAddUserToRole = validatorAddUserToRole;
        }

        [HttpPost()]
        public async ValueTask<ActionResult<IdentityRole>> Post([FromBody] RoleDataModel role)
        {

            if (string.IsNullOrEmpty(role.Name) || role == null) return BadRequest();

            if (!await roleManager.RoleExistsAsync(role.Name.Trim()))
            {
               var result = await roleManager.CreateAsync(new IdentityRole { Name = role.Name});
                if (result.Succeeded)
                {
                    var roleData = await roleManager.FindByNameAsync(role.Name);
                    
                    await userService.AddPermissionToRole(role.RoleClaims, roleData.Id);
                    return Ok(roleData);
                }
            }

            return BadRequest();
            
        }

        [HttpPut("{key}")]
        public async ValueTask<ActionResult<IdentityRole>> Put(string key, [FromBody] string roleName )
        {
            if (string.IsNullOrEmpty(roleName)) return BadRequest();

            var role = await roleManager.Roles.Where(x => x.Id == key).FirstOrDefaultAsync();
            if (!await roleManager.RoleExistsAsync(roleName.Trim())) 
            {
                if (role != null)
                {
                    role.Name = roleName;
                    var res =await roleManager.UpdateAsync(role);
                    if (res.Succeeded) return Ok();
                }
            }

            return NotFound();
        }

      
        [HttpPut("UpdateRoleClaims")]
        public async ValueTask<ActionResult<bool>> UpdateRoleClaims([FromBody] UpdateRolePermissionRequestDataModel dataModel)
        {
            if (dataModel == null) return BadRequest();

            var role = await roleManager.FindByIdAsync(dataModel.RoleId);

            if (role.Name == AppRoles.SuperAdmin)
            {
                ModelState.AddModelError("Role", "Super admin role can not be updated!");
                return ValidationProblem(instance: "103", modelStateDictionary: ModelState);
            }

            var result = await userService.UpdatePermissionAsync(dataModel);

            return result;


        }

        [HttpPost("AddUserToRole")]
        public async ValueTask<ActionResult> AddUserToRole([FromBody]AddUserToRoleDataModel dataModel)
        {
            var validatorResult = await validatorAddUserToRole.ValidateAsync(dataModel);

            if (!validatorResult.IsValid)
            {
                validatorResult.Errors.ForEach(x => ModelState.AddModelError("model", x.ErrorMessage));
                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            var user = await userManager.FindByEmailAsync(dataModel.Email);

            var role = await roleManager.FindByIdAsync(dataModel.RoleId);

            if (role == null)
            {
                ModelState.AddModelError("Role", "Role does not exist!");

                return ValidationProblem(instance: "100", modelStateDictionary: ModelState);
            }

            var result = await userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded) return Ok();
            else 
            {
                var validationError = new List<FluentValidation.Results.ValidationFailure>();
                var errorResult = new FluentValidation.Results.ValidationResult();
                foreach (var error in result.Errors)
                {
                    validationError.Add(new FluentValidation.Results.ValidationFailure(error.Code, error.Description));
                }
                errorResult.Errors = validationError;
                throw new Errors.ValidationException(errorResult);
            }


        }
    }
}
