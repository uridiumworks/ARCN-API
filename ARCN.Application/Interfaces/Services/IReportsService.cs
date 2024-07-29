using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IReportsService
    {
        ValueTask<ResponseModel<string>> AddReportsAsync(Reports model);
        ValueTask<ResponseModel<Reports>> GetAllReports();
        ValueTask<ResponseModel<Reports>> GetReportsById(int Reportsid);
        ValueTask<ResponseModel<string>> UpdateReportsAsync(int Reportsid, Reports model);
        ValueTask<ResponseModel<string>> DeleteReportsAsync(int Reportsid);
    }
}
