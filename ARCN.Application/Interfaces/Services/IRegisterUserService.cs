using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IRegisterUserService
    {
        ValueTask<ResponseModel<UserResponseDataModel>> Login(LoginDataModel loginDataModel);
        //ValueTask<UserResponseDataModel> CreateResponse(ApplicationUser user);
    }
}
