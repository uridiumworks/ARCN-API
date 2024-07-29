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
        ValueTask<ResponseModel<Blog>> AddBlogAsync(Blog model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<Blog>> GetAllBlog();
        ValueTask<ResponseModel<Blog>> GetBlogById(int blogid);
        ValueTask<ResponseModel<Blog>> UpdateBlogAsync(int blogid, BlogDataModel model);
        ValueTask<ResponseModel<string>> DeleteBlogAsync(int blogid);
    }
}
