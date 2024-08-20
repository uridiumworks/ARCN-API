using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IJournalsService
    {
        ValueTask<ResponseModel<Journals>> AddJournalsAsync(Journals model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<Journals>> GetAllJournals();
        ValueTask<ResponseModel<Journals>> GetJournalsById(int Journalsid);
        ValueTask<ResponseModel<Journals>> UpdateJournalsAsync(int Journalsid, JournalsDataModel model);
        ValueTask<ResponseModel<string>> DeleteJournalsAsync(int Journalsid);
        double GetAllJournalPreviousTotal();
        double GetAllJournalTotal();
    }
}
