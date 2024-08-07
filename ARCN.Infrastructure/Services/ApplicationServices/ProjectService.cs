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

        public ProjectService(
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IMapper mapper) {
            this.projectRepository = projectRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<Project>> AddProjectAsync(Project model, CancellationToken cancellationToken)
        {
            try
            {

                var result= await projectRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<Project> { Success = true, Message = "Successfully submitted", Data = result };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Project>
                {
                    Success = false,
                    Message = "Fail to insert",
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
        public async ValueTask<ResponseModel<Project>> UpdateProjectAsync(int Projectid, ProjectDataModel model)
        {
            try
            {
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
                        Data=res
                    };
                }
                else
                {
                    return new ResponseModel<Project>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Project>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteProjectAsync(int Projectid)
        {
            try
            {

                var Projects = await projectRepository.FindByIdAsync(Projectid);
                if (Projects != null)
                {
                    projectRepository.Remove(Projects);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Project Deleted  successfully",
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
