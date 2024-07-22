using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.FluentValidations.ApplicationUser
{
    public class VerifyOTPDataModelValidator:AbstractValidator<VerifyOTPDataModel>
    {
        public VerifyOTPDataModelValidator()
        {
            //RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.OTP).NotEmpty().Length(6);
        }
    }
}
