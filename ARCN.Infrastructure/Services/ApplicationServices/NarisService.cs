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
    public class NarisService:INarisService
    {
        private readonly INarisRepository narisRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public NarisService(
            INarisRepository narisRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,IMapper mapper) {
            this.narisRepository = narisRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<Naris>> AddNarisAsync(Naris model,CancellationToken cancellationToken)
        {
            try
            {

            //var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
            //if (user == null)
            //{
            //    return new ResponseModel<Naris>
            //    {
            //        Success = false,
            //        Message = "User not found",
            //    };
            //}

              var result= await  narisRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();
                return new ResponseModel<Naris>
                {
                    Success = true,
                    Message = "successfully created",
                    Data= result
                };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Naris>
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
        public async ValueTask<ResponseModel<Naris>> UpdateNarisAsync(int Narisid, NarisDataModel model)
        {
            try
            {

                //var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                //if (user == null)
                //{
                //    return new ResponseModel<Naris>
                //    {
                //        Success = false,
                //        Message = "User not found",
                //    };
                //}
                var Nariss = await narisRepository.FindByIdAsync(Narisid);
                if (Nariss != null)
                {
                    mapper.Map(model, Nariss);
                 var result=  narisRepository.Update(Nariss);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<Naris>
                    {
                        Success = true,
                        Message = "Update successfully",
                        Data = result
                    };
                }
                else
                {
                    return new ResponseModel<Naris>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Naris>
                {
                    Success = false,
                    Message = "Fail to Update",
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
