using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;
using MR3.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IRegisterUserService
    {
        ValueTask<ResponseModel<UserResponseDataModel>> Login(LoginDataModel loginDataModel);
        //ValueTask<UserResponseDataModel> CreateResponse(ApplicationUser user);
        ValueTask<ResponseModel<UserResponseDataModel>> ResetPassword(ResetPasswordModel resetPassword);
        ValueTask<ResponseModel<UserResponseDataModel>> ForgotPassword(ForgotPassword forgotPassword);
        ValueTask<ResponseModel<UserResponseDataModel>> ResetForgotPassword(ResetForgotPasswordModel resetPassword);
        ValueTask<ResponseModel<ApplicationUser>> ConfirmEmail(ConfirmEmailDataModel confirmEmail);
        ValueTask<ResponseModel<ApplicationUser>> AddUserPassword(AddAdmUserPasswordDataModel dataModel);
    }
}
