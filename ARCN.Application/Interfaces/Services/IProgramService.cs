using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IProgramService
    {
        ValueTask<ResponseModel<ARCNProgram>> AddProgramAsync(ARCNProgram model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<ARCNProgram>> GetAllProgram();
        ValueTask<ResponseModel<ARCNProgram>> GetProgramById(int Programid);
        ValueTask<ResponseModel<ARCNProgram>> UpdateProgramAsync(int Programid, ProgramDataModel model);
        ValueTask<ResponseModel<string>> DeleteProgramAsync(int Programid);
        double GetAllProgramPreviousTotal();
        double GetAllProgramTotal();
    }
}
