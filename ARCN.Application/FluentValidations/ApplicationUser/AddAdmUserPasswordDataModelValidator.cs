using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MR3.Application.DataModels.UserProfile;

namespace ARCN.Application.FluentValidations.ApplicationUser
{
    public class AddAdmUserPasswordDataModelValidator:AbstractValidator<AddAdmUserPasswordDataModel>
    {
        public AddAdmUserPasswordDataModelValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty()
                .Matches(r => r.ConfirmPassword).WithMessage("{PropertyName} must Match Confirm Password"); 
            RuleFor(x => x.ConfirmPassword).NotEmpty()
                .Matches(r => r.Password).WithMessage("{PropertyName} must Match Password"); 

        }
    }
}
