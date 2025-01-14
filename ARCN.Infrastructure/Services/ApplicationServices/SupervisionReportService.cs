﻿using System;
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
    public class SupervisionReportService:ISupervisionReportService
    {
        private readonly ISupervisionReportRepository supervisionReportRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IMapper mapper;
        private readonly IUserIdentityService userIdentityService;

        public SupervisionReportService(
            ISupervisionReportRepository supervisionReportRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IMapper mapper,IUserIdentityService userIdentityService) {
            this.supervisionReportRepository = supervisionReportRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.mapper = mapper;
            this.userIdentityService = userIdentityService;
        }
        public async ValueTask<ResponseModel<SupervisionReport>> AddSupervisionReportAsync(SupervisionReport model, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<SupervisionReport>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode=404
                    };
                }
                model.UserProfileId=user.Id;
                var result= await supervisionReportRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<SupervisionReport> { Success = true, Message = "Successfully submitted", Data = result,StatusCode=200 };

            }
            catch (Exception ex)
            {

                return new ResponseModel<SupervisionReport>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
        public async ValueTask<ResponseModel<SupervisionReport>> GetAllSupervisionReport()
        {
            var SupervisionReports =  supervisionReportRepository.FindAll();

            if (SupervisionReports == null)
                return ResponseModel<SupervisionReport>.ErrorMessage("SupervisionReports not found");

            return ResponseModel<SupervisionReport>.SuccessMessage(data: SupervisionReports);
        }
        public async ValueTask<ResponseModel<SupervisionReport>> GetSupervisionReportById(int SupervisionReportid)
        {
            var SupervisionReports = await supervisionReportRepository.FindByIdAsync(SupervisionReportid);

            if (SupervisionReports == null)
                return ResponseModel<SupervisionReport>.ErrorMessage("SupervisionReports not found");

            return ResponseModel<SupervisionReport>.SuccessMessage(data: SupervisionReports);
        }
        public async ValueTask<ResponseModel<SupervisionReport>> UpdateSupervisionReportAsync(int SupervisionReportid, SupervisionReportDataModel model)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<SupervisionReport>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode=404
                    };
                }
                var SupervisionReports = await supervisionReportRepository.FindByIdAsync(SupervisionReportid);
                if (SupervisionReports != null)
                {
                    mapper.Map(model, SupervisionReports);

                    var res= supervisionReportRepository.Update(SupervisionReports);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<SupervisionReport>
                    {
                        Success = true,
                        Message = "Updated successfully",
                        Data=res,
                        StatusCode=200
                    };
                }
                else
                {
                    return new ResponseModel<SupervisionReport>
                    {
                        Success = false,
                        Message = "Record not found",
                        StatusCode=404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<SupervisionReport>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode= 500
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteSupervisionReportAsync(int SupervisionReportid)
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
                var SupervisionReports = await supervisionReportRepository.FindByIdAsync(SupervisionReportid);
                if (SupervisionReports != null)
                {
                    supervisionReportRepository.Remove(SupervisionReports);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "SupervisionReport Deleted  successfully",
                        StatusCode=200
                    };
                }
                else
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Record not found",
                        StatusCode= 404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<string>
                {
                    Success = false,
                    Message =ex.Message,
                    StatusCode= 500
                };
            }
        }
    }
}
