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

        public BlogService(
            IBlogRepository blogRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService) {
            this.blogRepository = blogRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
        }
        public async ValueTask<ResponseModel<string>> AddBlogAsync(Blog model)
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
                };
            }

                unitOfWork.Add(model);
                unitOfWork.SaveChanges();
                return new ResponseModel<string>
                {
                    Success = true,
                    Message = "Update request successfully submitted",
                };

            }
            catch (Exception ex)
            {

                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Fail to insert",
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
        public async ValueTask<ResponseModel<string>> UpdateBlogAsync(int blogid, Blog model)
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
                    };
                }
                var blogs = await blogRepository.FindByIdAsync(blogid);
                if (blogs != null)
                {
                    unitOfWork.Update(model);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Update request successfully submitted",
                    };
                }
                else
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Fail to insert",
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
                    };
                }
                var blogs = await blogRepository.FindByIdAsync(blogid);
                if (blogs != null)
                {
                    unitOfWork.Remove(blogs);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Blog Deleted  successfully",
                    };
                }
                else
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Failed to delete",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Fail to Delete",
                };
            }
        }
    }
}
