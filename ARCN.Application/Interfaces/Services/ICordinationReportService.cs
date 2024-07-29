using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface ICordinationReportService
    {
        ValueTask<ResponseModel<string>> AddCordinationReportAsync(CordinationReport model);
        ValueTask<ResponseModel<CordinationReport>> GetAllCordinationReport();
        ValueTask<ResponseModel<CordinationReport>> GetCordinationReportById(int CordinationReportid);
        ValueTask<ResponseModel<string>> UpdateCordinationReportAsync(int CordinationReportid, CordinationReport model);
        ValueTask<ResponseModel<string>> DeleteCordinationReportAsync(int CordinationReportid);
    }
}
