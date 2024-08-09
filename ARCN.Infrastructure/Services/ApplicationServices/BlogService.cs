using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Repository.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class BlogService:IBlogService
    {
        private readonly IBlogRepository blogRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public BlogService(
            IBlogRepository blogRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,
            IMapper mapper) {
            this.blogRepository = blogRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<Blog>> AddBlogAsync(Blog model, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Blog>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode = 404
                    };
                }
                model.UserProfileId = userIdentityService.UserId;
                var result= await blogRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<Blog> { Success = true, Message = "Successfully submitted", Data = result, StatusCode = 200 };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Blog>
                {
                    Success = false,
                    Message = "Fail to insert",
                    StatusCode = 500
                };
            }
        }
        public async ValueTask<ResponseModel<Blog>> GetAllBlog()
        {
            var blogs =  blogRepository.FindAll();

            if (blogs == null)
                return ResponseModel<Blog>.ErrorMessage("blogs not found");

            return ResponseModel<Blog>.SuccessMessage(data: blogs);
        }
        public async ValueTask<ResponseModel<Blog>> GetBlogById(int blogid)
        {
            var blogs = await blogRepository.FindByIdAsync(blogid);

            if (blogs == null)
                return ResponseModel<Blog>.ErrorMessage("blogs not found");

            return ResponseModel<Blog>.SuccessMessage(data: blogs);
        }
        public async ValueTask<ResponseModel<Blog>> UpdateBlogAsync(int blogid, BlogDataModel model)
        {
            try
            {

                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Blog>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode = 404
                    };
                }
                var blogs = await blogRepository.FindByIdAsync(blogid);
                if (blogs != null)
                {
                    mapper.Map(model, blogs);

                    var res= blogRepository.Update(blogs);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<Blog>
                    {
                        Success = true,
                        Message = "Updated successfully",
                        Data=res,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseModel<Blog>
                    {
                        Success = false,
                        Message = "Record Not Found",
                        StatusCode = 404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Blog>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteBlogAsync(int blogid)
        {
            try
            {

                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode = 404
                    };
                }
                var blogs = await blogRepository.FindByIdAsync(blogid);
                if (blogs != null)
                {
                    blogRepository.Remove(blogs);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Blog Deleted  successfully",
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Record Not Found",
                        StatusCode = 404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
