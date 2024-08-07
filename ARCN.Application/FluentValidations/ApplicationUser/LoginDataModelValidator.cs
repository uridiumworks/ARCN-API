using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.FluentValidations.ApplicationUser
{
    public class LoginDataModelValidator : AbstractValidator<LoginDataModel>
    {
        public LoginDataModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Length(6, 20);
        }
    }
}
