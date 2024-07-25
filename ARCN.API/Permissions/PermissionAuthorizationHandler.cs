
using ARCN.Domain.Commons.Authorization;

namespace ARCN.API.Permissions
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User is null) await Task.CompletedTask; 

            var permission = context.User.Claims
                .Where(x => x.Type == AppClaim.Permission 
                && x.Value == requirement.Permission
                && x.Issuer == "LOCAL AUTHORITY");

            if (permission.Any()) 
            {
                context.Succeed(requirement);
                await Task.CompletedTask;
            }
        }
    }
}
