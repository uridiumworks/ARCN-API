using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface INewsLetterService
    {
        ValueTask<ResponseModel<NewsLetter>> AddNewsLetterAsync(NewsLetter model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<NewsLetter>> GetAllNewsLetter();
        ValueTask<ResponseModel<NewsLetter>> GetNewsLetterById(int NewsLetterid);
        ValueTask<ResponseModel<NewsLetter>> UpdateNewsLetterAsync(int NewsLetterid, NewsLetterDataModel model);
        ValueTask<ResponseModel<string>> DeleteNewsLetterAsync(int NewsLetterid);
    }
}
