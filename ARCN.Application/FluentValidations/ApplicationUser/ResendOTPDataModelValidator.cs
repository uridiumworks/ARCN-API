using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ApplicationDataModels;


namespace ARCN.Application.FluentValidations.ApplicationUser
{
    public class ResendOTPDataModelValidator:AbstractValidator<ResendOTPDataModel>
    {
        public ResendOTPDataModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
