using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface INarisService
    {
        ValueTask<ResponseModel<string>> AddNarisAsync(Naris model);
        ValueTask<ResponseModel<Naris>> GetAllNaris();
        ValueTask<ResponseModel<Naris>> GetNarisById(int Narisid);
        ValueTask<ResponseModel<string>> UpdateNarisAsync(int Narisid, Naris model);
        ValueTask<ResponseModel<string>> DeleteNarisAsync(int Narisid);
    }
}
