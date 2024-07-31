using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IFCAService
    {
        ValueTask<ResponseModel<FCA>> AddFCAAsync(FCA model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<FCA>> GetAllFCA();
        ValueTask<ResponseModel<FCA>> GetFCAById(int FCAid);
        ValueTask<ResponseModel<FCA>> UpdateFCAAsync(int FCAid, FCADataModel model);
        ValueTask<ResponseModel<string>> DeleteFCAAsync(int FCAid);
    }
}
