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
    public class NarisService:INarisService
    {
        private readonly INarisRepository narisRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;

        public NarisService(
            INarisRepository narisRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService) {
            this.narisRepository = narisRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
        }
        public async ValueTask<ResponseModel<string>> AddNarisAsync(Naris model)
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
        public async ValueTask<ResponseModel<Naris>> GetAllNaris()
        {
            var Nariss =  narisRepository.FindAll();

            if (Nariss == null)
                return ResponseModel<Naris>.ErrorMessage("Nariss not found");

            return ResponseModel<Naris>.SuccessMessage(data: Nariss);
        }
        public async ValueTask<ResponseModel<Naris>> GetNarisById(int Narisid)
        {
            var Nariss = await narisRepository.FindByIdAsync(Narisid);

            if (Nariss == null)
                return ResponseModel<Naris>.ErrorMessage("Nariss not found");

            return ResponseModel<Naris>.SuccessMessage(data: Nariss);
        }
        public async ValueTask<ResponseModel<string>> UpdateNarisAsync(int Narisid, Naris model)
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
                var Nariss = await narisRepository.FindByIdAsync(Narisid);
                if (Nariss != null)
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

        public async ValueTask<ResponseModel<string>> DeleteNarisAsync(int Narisid)
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
                var Nariss = await narisRepository.FindByIdAsync(Narisid);
                if (Nariss != null)
                {
                    unitOfWork.Remove(Nariss);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Naris Deleted  successfully",
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
