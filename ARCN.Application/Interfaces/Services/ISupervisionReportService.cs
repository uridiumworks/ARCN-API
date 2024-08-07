using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface ISupervisionReportService
    {
        ValueTask<ResponseModel<SupervisionReport>> AddSupervisionReportAsync(SupervisionReport model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<SupervisionReport>> GetAllSupervisionReport();
        ValueTask<ResponseModel<SupervisionReport>> GetSupervisionReportById(int SupervisionReportid);
        ValueTask<ResponseModel<SupervisionReport>> UpdateSupervisionReportAsync(int SupervisionReportid, SupervisionReportDataModel model);
        ValueTask<ResponseModel<string>> DeleteSupervisionReportAsync(int SupervisionReportid);
    }
}
