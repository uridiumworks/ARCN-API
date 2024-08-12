using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using ARCN.Repository.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class ProjectService:IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IMapper mapper;
        private readonly IUserIdentityService userIdentityService;

        public ProjectService(
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IMapper mapper,IUserIdentityService userIdentityService) {
            this.projectRepository = projectRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.mapper = mapper;
            this.userIdentityService = userIdentityService;
        }
        public async ValueTask<ResponseModel<Project>> AddProjectAsync(Project model, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Project>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode=404
                    };
                }
                model.UserProfileId=user.Id;
                var result= await projectRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<Project> { Success = true, Message = "Successfully submitted", Data = result,StatusCode=200 };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Project>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode= 500
                };
            }
        }
        public async ValueTask<ResponseModel<Project>> GetAllProject()
        {
            var Projects =  projectRepository.FindAll();

            if (Projects == null)
                return ResponseModel<Project>.ErrorMessage("Projects not found");

            return ResponseModel<Project>.SuccessMessage(data: Projects);
        }
        public async ValueTask<ResponseModel<Project>> GetProjectById(int Projectid)
        {
            var Projects = await projectRepository.FindByIdAsync(Projectid);

            if (Projects == null)
                return ResponseModel<Project>.ErrorMessage("Projects not found");

            return ResponseModel<Project>.SuccessMessage(data: Projects);
        }
        public double GetAllProjectTotal()
        {
            var project = projectRepository.FindAll().Where(x => x.CreatedDate < DateTime.Now.Date.AddMonths(-1)).Count();
            return project;
        }
        public double GetAllProjectPreviousTotal()
        {
            var project = projectRepository.FindAll().Where(x => x.CreatedDate > DateTime.Now.Date.AddMonths(-1)).Count();
            return project;
        }
        public async ValueTask<ResponseModel<Project>> UpdateProjectAsync(int Projectid, ProjectDataModel model)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Project>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode=404
                    };
                }
                var Projects = await projectRepository.FindByIdAsync(Projectid);
                if (Projects != null)
                {
                    mapper.Map(model, Projects);

                    var res= projectRepository.Update(Projects);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<Project>
                    {
                        Success = true,
                        Message = "Updated successfully",
                        Data=res,
                        StatusCode= 200
                    };
                }
                else
                {
                    return new ResponseModel<Project>
                    {
                        Success = false,
                        Message = "Record not found",
                        StatusCode=404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Project>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteProjectAsync(int Projectid)
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
                        StatusCode=404
                    };
                }
                var Projects = await projectRepository.FindByIdAsync(Projectid);
                if (Projects != null)
                {
                    projectRepository.Remove(Projects);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Project Deleted  successfully",
                        StatusCode=200
                    };
                }
                else
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Record not found",
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
