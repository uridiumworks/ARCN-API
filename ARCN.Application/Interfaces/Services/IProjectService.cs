using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IProjectService
    {
        ValueTask<ResponseModel<Project>> AddProjectAsync(Project model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<Project>> GetAllProject();
        ValueTask<ResponseModel<Project>> GetProjectById(int Projectid);
        ValueTask<ResponseModel<Project>> UpdateProjectAsync(int Projectid, ProjectDataModel model);
        ValueTask<ResponseModel<string>> DeleteProjectAsync(int Projectid);
    }
}
