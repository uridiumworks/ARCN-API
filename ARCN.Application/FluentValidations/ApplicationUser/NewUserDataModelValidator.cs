

using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.FluentValidations.ApplicationUser
{
    public class NewUserDataModelValidator:AbstractValidator<NewUserDataModel>
    {
        public NewUserDataModelValidator()
        {
           // RuleFor(x => x.BVN).NotEmpty().Length(11).WithMessage("{PropertyName} must be 11 digits");
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PhoneNumber).NotEmpty().Length(10,11);
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
