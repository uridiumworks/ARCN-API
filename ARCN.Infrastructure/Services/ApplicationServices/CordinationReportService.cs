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
    public class CordinationReportService:ICordinationReportService
    {
        private readonly ICordinationReportRepository cordinationReportRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public CordinationReportService(
            ICordinationReportRepository cordinationReportRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,
            IMapper mapper) {
            this.cordinationReportRepository = cordinationReportRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<CordinationReport>> AddCordinationReportAsync(CordinationReport model,CancellationToken cancellationToken)
        {
            try
            {

                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<CordinationReport>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode = 404
                    };
                }
                model.UserProfileId = user.Id;
                var result=  await cordinationReportRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();
                return new ResponseModel<CordinationReport>
                {
                    Success = true,
                    Message = "Update request successfully submitted",
                    Data= result,
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {

                return new ResponseModel<CordinationReport>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
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
        public async ValueTask<ResponseModel<CordinationReport>> UpdateCordinationReportAsync(int CordinationReportid, CordinationReportDataModel model)
        {
            try
            {

                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<CordinationReport>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode = 404
                    };
                }
                var CordinationReports = await cordinationReportRepository.FindByIdAsync(CordinationReportid);
                if (CordinationReports != null)
                {
                    mapper.Map(model, CordinationReports);
                   var result= cordinationReportRepository.Update(CordinationReports);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<CordinationReport>
                    {
                        Success = true,
                        Message = "Update successfully submitted",
                        Data = result,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseModel<CordinationReport>
                    {
                        Success = false,
                        Message = "Record Not Found",
                        StatusCode = 404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<CordinationReport  >
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode=500
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
                        StatusCode = 404
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
