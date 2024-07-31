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
    public class FCAService:IFCAService
    {
        private readonly IFCARepository FCARepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public FCAService(
            IFCARepository FCARepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,
            IMapper mapper) {
            this.FCARepository = FCARepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<FCA>> AddFCAAsync(FCA model, CancellationToken cancellationToken)
        {
            try
            {
                var result= await FCARepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<FCA> { Success = true, Message = "Successfully submitted", Data = result };

            }
            catch (Exception ex)
            {

                return new ResponseModel<FCA>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }
        public async ValueTask<ResponseModel<FCA>> GetAllFCA()
        {
            var FCAs =  FCARepository.FindAll();

            if (FCAs == null)
                return ResponseModel<FCA>.ErrorMessage("FCAs not found");

            return ResponseModel<FCA>.SuccessMessage(data: FCAs);
        }
        public async ValueTask<ResponseModel<FCA>> GetFCAById(int FCAid)
        {
            var FCAs = await FCARepository.FindByIdAsync(FCAid);

            if (FCAs == null)
                return ResponseModel<FCA>.ErrorMessage("FCAs not found");

            return ResponseModel<FCA>.SuccessMessage(data: FCAs);
        }
        public async ValueTask<ResponseModel<FCA>> UpdateFCAAsync(int FCAid, FCADataModel model)
        {
            try
            {
                var FCAs = await FCARepository.FindByIdAsync(FCAid);
                if (FCAs != null)
                {
                    mapper.Map(model, FCAs);

                    var res= FCARepository.Update(FCAs);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<FCA>
                    {
                        Success = true,
                        Message = "Updated successfully",
                        Data=res
                    };
                }
                else
                {
                    return new ResponseModel<FCA>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<FCA>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteFCAAsync(int FCAid)
        {
            try
            {

                var FCAs = await FCARepository.FindByIdAsync(FCAid);
                if (FCAs != null)
                {
                    FCARepository.Remove(FCAs);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "FCA Deleted  successfully",
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
