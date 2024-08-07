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

        public EntrepreneurshipService(
            IEntrepreneurshipRepository entrepreneurshipRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IMapper mapper) {
            this.entrepreneurshipRepository = entrepreneurshipRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<Entrepreneurship>> AddEntrepreneurshipAsync(Entrepreneurship model, CancellationToken cancellationToken)
        {
            try
            {

                var result= await entrepreneurshipRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<Entrepreneurship> { Success = true, Message = "Successfully submitted", Data = result };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Entrepreneurship>
                {
                    Success = false,
                    Message = "Fail to insert",
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
                        Data=res
                    };
                }
                else
                {
                    return new ResponseModel<Entrepreneurship>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Entrepreneurship>
                {
                    Success = false,
                    Message = "Fail to insert",
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
