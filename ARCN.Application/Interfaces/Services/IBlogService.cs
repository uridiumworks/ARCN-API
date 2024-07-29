using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IBlogService
    {
        ValueTask<ResponseModel<string>> AddBlogAsync(Blog model);
        ValueTask<ResponseModel<Blog>> GetAllBlog();
        ValueTask<ResponseModel<Blog>> GetBlogById(int blogid);
        ValueTask<ResponseModel<string>> UpdateBlogAsync(int blogid, Blog model);
        ValueTask<ResponseModel<string>> DeleteBlogAsync(int blogid);
    }
}
