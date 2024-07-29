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
    public class JournalService:IJournalsService
    {
        private readonly IJournalRepository journalRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;

        public JournalService(
            IJournalRepository journalRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService) {
            this.journalRepository = journalRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
        }
        public async ValueTask<ResponseModel<string>> AddJournalsAsync(Journals model)
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
        public async ValueTask<ResponseModel<Journals>> GetAllJournals()
        {
            var Journalss =  journalRepository.FindAll();

            if (Journalss == null)
                return ResponseModel<Journals>.ErrorMessage("Journalss not found");

            return ResponseModel<Journals>.SuccessMessage(data: Journalss);
        }
        public async ValueTask<ResponseModel<Journals>> GetJournalsById(int Journalsid)
        {
            var Journalss = await journalRepository.FindByIdAsync(Journalsid);

            if (Journalss == null)
                return ResponseModel<Journals>.ErrorMessage("Journalss not found");

            return ResponseModel<Journals>.SuccessMessage(data: Journalss);
        }
        public async ValueTask<ResponseModel<string>> UpdateJournalsAsync(int Journalsid, Journals model)
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
                var Journalss = await journalRepository.FindByIdAsync(Journalsid);
                if (Journalss != null)
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

        public async ValueTask<ResponseModel<string>> DeleteJournalsAsync(int Journalsid)
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
                var Journalss = await journalRepository.FindByIdAsync(Journalsid);
                if (Journalss != null)
                {
                    unitOfWork.Remove(Journalss);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Journals Deleted  successfully",
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
