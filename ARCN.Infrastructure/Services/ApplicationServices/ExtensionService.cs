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
    public class ExtensionService:IExtensionService
    {
        private readonly IExtensionRepository ExtensionRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserprofileService userprofileService;
        private readonly IUserIdentityService userIdentityService;
        private readonly IMapper mapper;

        public ExtensionService(
            IExtensionRepository ExtensionRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IUserprofileService userprofileService,
            IUserIdentityService userIdentityService,
            IMapper mapper) {
            this.ExtensionRepository = ExtensionRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.userprofileService = userprofileService;
            this.userIdentityService = userIdentityService;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<Extension>> AddExtensionAsync(Extension model, CancellationToken cancellationToken)
        {
            try
            {

                var result= await ExtensionRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<Extension> { Success = true, Message = "Successfully submitted", Data = result };

            }
            catch (Exception ex)
            {

                return new ResponseModel<Extension>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }
        public async ValueTask<ResponseModel<Extension>> GetAllExtension()
        {
            var Extensions =  ExtensionRepository.FindAll();

            if (Extensions == null)
                return ResponseModel<Extension>.ErrorMessage("Extensions not found");

            return ResponseModel<Extension>.SuccessMessage(data: Extensions);
        }
        public async ValueTask<ResponseModel<Extension>> GetExtensionById(int Extensionid)
        {
            var Extensions = await ExtensionRepository.FindByIdAsync(Extensionid);

            if (Extensions == null)
                return ResponseModel<Extension>.ErrorMessage("Extensions not found");

            return ResponseModel<Extension>.SuccessMessage(data: Extensions);
        }
        public async ValueTask<ResponseModel<Extension>> UpdateExtensionAsync(int Extensionid, ExtensionDataModel model)
        {
            try
            {
                var Extensions = await ExtensionRepository.FindByIdAsync(Extensionid);
                if (Extensions != null)
                {
                    mapper.Map(model, Extensions);

                    var res= ExtensionRepository.Update(Extensions);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<Extension>
                    {
                        Success = true,
                        Message = "Updated successfully",
                        Data=res
                    };
                }
                else
                {
                    return new ResponseModel<Extension>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<Extension>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteExtensionAsync(int Extensionid)
        {
            try
            {

                var Extensions = await ExtensionRepository.FindByIdAsync(Extensionid);
                if (Extensions != null)
                {
                    ExtensionRepository.Remove(Extensions);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Extension Deleted  successfully",
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
