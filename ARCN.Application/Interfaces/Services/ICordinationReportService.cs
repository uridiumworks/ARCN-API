using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface ICordinationReportService
    {
        ValueTask<ResponseModel<CordinationReport>> AddCordinationReportAsync(CordinationReport model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<CordinationReport>> GetAllCordinationReport();
        ValueTask<ResponseModel<CordinationReport>> GetCordinationReportById(int CordinationReportid);
        ValueTask<ResponseModel<CordinationReport>> UpdateCordinationReportAsync(int CordinationReportid, CordinationReportDataModel model);
        ValueTask<ResponseModel<string>> DeleteCordinationReportAsync(int CordinationReportid);
    }
}
