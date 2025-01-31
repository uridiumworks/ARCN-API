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
    public class EntrepreneurshipService:IEntrepreneurshipService
    {
        private readonly IEntrepreneurshipRepository entrepreneurshipRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IMapper mapper;
        private readonly IUserIdentityService userIdentityService;

        public EntrepreneurshipService(
            IEntrepreneurshipRepository entrepreneurshipRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IMapper mapper,IUserIdentityService userIdentityService) {
            this.entrepreneurshipRepository = entrepreneurshipRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.mapper = mapper;
            this.userIdentityService = userIdentityService;
        }
        public async ValueTask<ResponseModel<Entrepreneurship>> AddEntrepreneurshipAsync(Entrepreneurship model, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Entrepreneurship>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode=404
                    };
                }
                model.UserProfileId = user.Id;
                var result= await entrepreneurshipRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<Entrepreneurship> { Success = true, Message = "Successfully submitted", Data = result,StatusCode=200};

            }
            catch (Exception ex)
            {

                return new ResponseModel<Entrepreneurship>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode= 500
                };
            }
        }
        public async ValueTask<ResponseModel<Entrepreneurship>> GetAllEntrepreneurship()
        {
            var Entrepreneurships =  entrepreneurshipRepository.FindAll();

            if (Entrepreneurships == null)
                return ResponseModel<Entrepreneurship>.ErrorMessage("Entrepreneurships not found");

            return ResponseModel<Entrepreneurship>.SuccessMessage(data: Entrepreneurships);
        }
        public async ValueTask<ResponseModel<Entrepreneurship>> GetEntrepreneurshipById(int Entrepreneurshipid)
        {
            var Entrepreneurships = await entrepreneurshipRepository.FindByIdAsync(Entrepreneurshipid);

            if (Entrepreneurships == null)
                return ResponseModel<Entrepreneurship>.ErrorMessage("Entrepreneurships not found");

            return ResponseModel<Entrepreneurship>.SuccessMessage(data: Entrepreneurships);
        }
        public async ValueTask<ResponseModel<Entrepreneurship>> UpdateEntrepreneurshipAsync(int Entrepreneurshipid, EntrepreneurshipDataModel model)
        {
            try
            {
                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Entrepreneurship>
                    {
                        Success = false,
                        Message = "User not found",
                        StatusCode=404
                    };
                }
                 var Entrepreneurships = await entrepreneurshipRepository.FindByIdAsync(Entrepreneurshipid);
                if (Entrepreneurships != null)
                {
                    mapper.Map(model, Entrepreneurships);

                    var res= entrepreneurshipRepository.Update(Entrepreneurships);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<Entrepreneurship>
                    {
                        Success = true,
                        Message = "Updated successfully",
                        Data=res,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseModel<Entrepreneurship>
                    {
                        Success = false,
                        Message = "Record Not Found",
                        StatusCode=404
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Entrepreneurship>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteEntrepreneurshipAsync(int Entrepreneurshipid)
        {
            try
            {

                var Entrepreneurships = await entrepreneurshipRepository.FindByIdAsync(Entrepreneurshipid);
                if (Entrepreneurships != null)
                {
                    entrepreneurshipRepository.Remove(Entrepreneurships);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Entrepreneurship Deleted  successfully",
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResponseModel<string>
                    {
                        Success = false,
                        Message = "Failed to delete",
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
