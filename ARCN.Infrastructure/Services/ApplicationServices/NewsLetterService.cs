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
    public class NewsLetterService:INewsLetterService
    {
        private readonly INewsLetterRepository newsLetterRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public NewsLetterService(
            INewsLetterRepository newsLetterRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,IMapper mapper) {
            this.newsLetterRepository = newsLetterRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<NewsLetter>> AddNewsLetterAsync(NewsLetter model,CancellationToken cancellationToken)
        {
            try
            {

            var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
            if (user == null)
            {
                return new ResponseModel<NewsLetter>
                {
                    Success = false,
                    Message = "User not found",
                };
            }

               var result= await newsLetterRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();
                return new ResponseModel<NewsLetter>
                {
                    Success = true,
                    Message = "Update request successfully submitted",
                    Data=result
                };

            }
            catch (Exception ex)
            {

                return new ResponseModel<NewsLetter>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }
        public async ValueTask<ResponseModel<NewsLetter>> GetAllNewsLetter()
        {
            var NewsLetters =  newsLetterRepository.FindAll();

            if (NewsLetters == null)
                return ResponseModel<NewsLetter>.ErrorMessage("NewsLetters not found");

            return ResponseModel<NewsLetter>.SuccessMessage(data: NewsLetters);
        }
        public async ValueTask<ResponseModel<NewsLetter>> GetNewsLetterById(int NewsLetterid)
        {
            var NewsLetters = await newsLetterRepository.FindByIdAsync(NewsLetterid);

            if (NewsLetters == null)
                return ResponseModel<NewsLetter>.ErrorMessage("NewsLetters not found");

            return ResponseModel<NewsLetter>.SuccessMessage(data: NewsLetters);
        }
        public async ValueTask<ResponseModel<NewsLetter>> UpdateNewsLetterAsync(int NewsLetterid, NewsLetterDataModel model)
        {
            try
            {

                //var user = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);
                //if (user == null)
                //{
                //    return new ResponseModel<NewsLetter>
                //    {
                //        Success = false,
                //        Message = "User not found",
                //    };
                //}
                var NewsLetters = await newsLetterRepository.FindByIdAsync(NewsLetterid);
                if (NewsLetters != null)
                {
                    mapper.Map(model, NewsLetters);
                 var result=   newsLetterRepository.Update(NewsLetters);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<NewsLetter>
                    {
                        Success = true,
                        Message = "Update request successfully submitted",
                        Data=result
                    };
                }
                else
                {
                    return new ResponseModel<NewsLetter>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<NewsLetter>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteNewsLetterAsync(int NewsLetterid)
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
                var NewsLetters = await newsLetterRepository.FindByIdAsync(NewsLetterid);
                if (NewsLetters != null)
                {
                    unitOfWork.Remove(NewsLetters);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "NewsLetter Deleted  successfully",
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
