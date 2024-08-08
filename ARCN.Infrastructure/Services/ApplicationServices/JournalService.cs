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
    public class JournalService:IJournalsService
    {
        private readonly IJournalRepository journalRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public JournalService(
            IJournalRepository journalRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,IMapper mapper) {
            this.journalRepository = journalRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<Journals>> AddJournalsAsync(Journals model,CancellationToken cancellationToken)
        {
            try
            {

                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Journals>
                    {
                        Success = false,
                        Message = "User not found",
                    };
                }
                model.UserProfileId = user.Id;
                var result= await journalRepository.AddAsync(model, cancellationToken);
                unitOfWork.SaveChanges();
                return new ResponseModel<Journals>
                {
                    Success = true,
                    Message = "successfully submitted",
                    Data= result
                };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Journals>
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
        public double GetAllJournalTotal()
        {
            var journal = journalRepository.FindAll().Where(x => x.CreatedDate < DateTime.Now.Date.AddMonths(-1)).Count();
            return journal;
        }
        public double GetAllJournalPreviousTotal()
        {
            var journal = journalRepository.FindAll().Where(x => x.CreatedDate > DateTime.Now.Date.AddMonths(-1)).Count();
            return journal;
        }
        public async ValueTask<ResponseModel<Journals>> UpdateJournalsAsync(int Journalsid, JournalsDataModel model)
        {
            try
            {

                var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                if (user == null)
                {
                    return new ResponseModel<Journals>
                    {
                        Success = false,
                        Message = "User not found",
                    };
                }
                var Journalss = await journalRepository.FindByIdAsync(Journalsid);
                if (Journalss != null)
                {
                    mapper.Map(model, Journalss);
                  var result=  journalRepository.Update(Journalss);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<Journals>
                    {
                        Success = true,
                        Message = "Update successfully submitted",
                        Data=result
                    };
                }
                else
                {
                    return new ResponseModel<Journals>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Journals>
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
