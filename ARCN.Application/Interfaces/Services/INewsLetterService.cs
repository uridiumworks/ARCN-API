using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface INewsLetterService
    {
        ValueTask<ResponseModel<string>> AddNewsLetterAsync(NewsLetter model);
        ValueTask<ResponseModel<NewsLetter>> GetAllNewsLetter();
        ValueTask<ResponseModel<NewsLetter>> GetNewsLetterById(int NewsLetterid);
        ValueTask<ResponseModel<string>> UpdateNewsLetterAsync(int NewsLetterid, NewsLetter model);
        ValueTask<ResponseModel<string>> DeleteNewsLetterAsync(int NewsLetterid);
    }
}
