using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;

namespace NovaBank.Application.FluentValidations.ApplicationUser
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordModel>
    {
        private readonly IUnitOfWork unitOfWork;

        public ResetPasswordValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            RuleFor(r => r.NewPassword)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Matches(r => r.NewPassword).WithMessage("{PropertyName} must Matches");
    
        }



    }
}
