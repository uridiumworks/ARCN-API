
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.FluentValidations.ApplicationUser
{
    public class AddUserToRoleDataModelValidator : AbstractValidator<AddUserToRoleDataModel>
    {
        public AddUserToRoleDataModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.RoleId).NotEmpty();
        }
    }
}
