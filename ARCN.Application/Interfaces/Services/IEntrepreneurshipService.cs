using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IEntrepreneurshipService
    {
        ValueTask<ResponseModel<Entrepreneurship>> AddEntrepreneurshipAsync(Entrepreneurship model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<Entrepreneurship>> GetAllEntrepreneurship();
        ValueTask<ResponseModel<Entrepreneurship>> GetEntrepreneurshipById(int Entrepreneurshipid);
        ValueTask<ResponseModel<Entrepreneurship>> UpdateEntrepreneurshipAsync(int Entrepreneurshipid, EntrepreneurshipDataModel model);
        ValueTask<ResponseModel<string>> DeleteEntrepreneurshipAsync(int Entrepreneurshipid);
    }
}
