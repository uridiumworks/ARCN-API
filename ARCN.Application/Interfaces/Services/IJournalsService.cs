using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IJournalsService
    {
        ValueTask<ResponseModel<string>> AddJournalsAsync(Journals model);
        ValueTask<ResponseModel<Journals>> GetAllJournals();
        ValueTask<ResponseModel<Journals>> GetJournalsById(int Journalsid);
        ValueTask<ResponseModel<string>> UpdateJournalsAsync(int Journalsid, Journals model);
        ValueTask<ResponseModel<string>> DeleteJournalsAsync(int Journalsid);
    }
}
