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
using Microsoft.AspNetCore.Identity;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class ReportsService:IReportsService
    {

        private readonly IReportsRepository reportsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public ReportsService(
            IReportsRepository reportsRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,IMapper mapper) {
            this.reportsRepository = reportsRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<Reports>> AddReportsAsync(Reports model,CancellationToken cancellationToken)
        {
            try
            {

            //var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
            //if (user == null)
            //{
            //    return new ResponseModel<Reports>
            //    {
            //        Success = false,
            //        Message = "User not found",
            //    };
            //}

               var result= await reportsRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();
                return new ResponseModel<Reports>
                {
                    Success = true,
                    Message = "Update request successfully submitted",
                    Data = result
                };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Reports>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }
        public async ValueTask<ResponseModel<Reports>> GetAllReports()
        {
            var Reportss =  reportsRepository.FindAll();

            if (Reportss == null)
                return ResponseModel<Reports>.ErrorMessage("Reportss not found");

            return ResponseModel<Reports>.SuccessMessage(data: Reportss);
        }
        public async ValueTask<ResponseModel<Reports>> GetReportsById(int Reportsid)
        {
            var Reportss = await reportsRepository.FindByIdAsync(Reportsid);

            if (Reportss == null)
                return ResponseModel<Reports>.ErrorMessage("Reportss not found");

            return ResponseModel<Reports>.SuccessMessage(data: Reportss);
        }
        public async ValueTask<ResponseModel<Reports>> UpdateReportsAsync(int Reportsid, ReportsDataModel model)
        {
            try
            {

                //var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                //if (user == null)
                //{
                //    return new ResponseModel<Reports>
                //    {
                //        Success = false,
                //        Message = "User not found",
                //    };
                //}
                var Reportss = await reportsRepository.FindByIdAsync(Reportsid);
                if (Reportss != null)
                {
                    mapper.Map(model, Reportss);
                  var result=  reportsRepository.Update(Reportss);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<Reports>
                    {
                        Success = true,
                        Message = "Update request successfully submitted",
                    };
                }
                else
                {
                    return new ResponseModel<Reports>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Reports>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteReportsAsync(int Reportsid)
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
                var Reportss = await reportsRepository.FindByIdAsync(Reportsid);
                if (Reportss != null)
                {
                    unitOfWork.Remove(Reportss);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Reports Deleted  successfully",
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
