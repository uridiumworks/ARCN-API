using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface INarisService
    {
        ValueTask<ResponseModel<Naris>> AddNarisAsync(Naris model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<Naris>> GetAllNaris();
        ValueTask<ResponseModel<Naris>> GetNarisById(int Narisid);
        ValueTask<ResponseModel<Naris>> UpdateNarisAsync(int Narisid, NarisDataModel model);
        ValueTask<ResponseModel<string>> DeleteNarisAsync(int Narisid);
    }
}
