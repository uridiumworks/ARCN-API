
using ARCN.Application.DataModels.UserProfile;


namespace ARCN.Application.FluentValidations.UserProfile
{
    public class ProfileUpdateDataModelValidator:AbstractValidator<ProfileUpdateDataModel>
    {
        public ProfileUpdateDataModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty().Length(10,11);
        }
    }
}
