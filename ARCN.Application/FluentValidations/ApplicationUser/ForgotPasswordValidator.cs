using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;


namespace ARCN.Application.FluentValidations.ApplicationUser
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPassword>
    {
        private readonly IUnitOfWork unitOfWork;

        public ForgotPasswordValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            RuleFor(r => r.email)
                .NotEmpty().WithMessage("{PropertyName} is required");

        }



    }
}
