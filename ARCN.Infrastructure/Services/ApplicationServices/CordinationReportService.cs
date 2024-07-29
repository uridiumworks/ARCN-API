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
    public class CordinationReportService:ICordinationReportService
    {
        private readonly ICordinationReportRepository cordinationReportRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;

        public CordinationReportService(
            ICordinationReportRepository cordinationReportRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService) {
            this.cordinationReportRepository = cordinationReportRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
        }
        public async ValueTask<ResponseModel<string>> AddCordinationReportAsync(CordinationReport model)
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
        public async ValueTask<ResponseModel<CordinationReport>> GetAllCordinationReport()
        {
            var CordinationReports =  cordinationReportRepository.FindAll();

            if (CordinationReports == null)
                return ResponseModel<CordinationReport>.ErrorMessage("CordinationReports not found");

            return ResponseModel<CordinationReport>.SuccessMessage(data: CordinationReports);
        }
        public async ValueTask<ResponseModel<CordinationReport>> GetCordinationReportById(int CordinationReportid)
        {
            var CordinationReports = await cordinationReportRepository.FindByIdAsync(CordinationReportid);

            if (CordinationReports == null)
                return ResponseModel<CordinationReport>.ErrorMessage("CordinationReports not found");

            return ResponseModel<CordinationReport>.SuccessMessage(data: CordinationReports);
        }
        public async ValueTask<ResponseModel<string>> UpdateCordinationReportAsync(int CordinationReportid, CordinationReport model)
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
                var CordinationReports = await cordinationReportRepository.FindByIdAsync(CordinationReportid);
                if (CordinationReports != null)
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

        public async ValueTask<ResponseModel<string>> DeleteCordinationReportAsync(int CordinationReportid)
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
                var CordinationReports = await cordinationReportRepository.FindByIdAsync(CordinationReportid);
                if (CordinationReports != null)
                {
                    unitOfWork.Remove(CordinationReports);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "CordinationReport Deleted  successfully",
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
