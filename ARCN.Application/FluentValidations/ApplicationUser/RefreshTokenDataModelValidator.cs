using ARCN.Application.DataModels.ApplicationDataModels;

namespace ARCN.Application.FluentValidations.ApplicationUser
{
    public class RefreshTokenDataModelValidator:AbstractValidator<RefreshTokenDataModel>
    {
        public RefreshTokenDataModelValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
            RuleFor(x=>x.ApplicationUserId).NotEmpty();
        }
    }
}
