using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IReportsService
    {
        ValueTask<ResponseModel<Reports>> AddReportsAsync(Reports model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<Reports>> GetAllReports();
        ValueTask<ResponseModel<Reports>> GetReportsById(int Reportsid);
        ValueTask<ResponseModel<Reports>> UpdateReportsAsync(int Reportsid, ReportsDataModel model);
        ValueTask<ResponseModel<string>> DeleteReportsAsync(int Reportsid);
    }
}
