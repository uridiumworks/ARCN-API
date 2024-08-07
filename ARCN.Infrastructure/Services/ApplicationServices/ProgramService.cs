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
    public class ProgramService:IProgramService
    {
        private readonly IProgramRepository programRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IMapper mapper;

        public ProgramService(
            IProgramRepository programRepository,
            IUnitOfWork unitOfWork,
            IUserProfileRepository userProfileRepository,
            IMapper mapper) {
            this.programRepository = programRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
            this.mapper = mapper;
        }
        public async ValueTask<ResponseModel<ARCNProgram>> AddProgramAsync(ARCNProgram model, CancellationToken cancellationToken)
        {
            try
            {

                var result= await programRepository.AddAsync(model,cancellationToken);
                unitOfWork.SaveChanges();

                return new ResponseModel<ARCNProgram> { Success = true, Message = "Successfully submitted", Data = result };

            }
            catch (Exception ex)
            {

                return new ResponseModel<ARCNProgram>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }
        public async ValueTask<ResponseModel<ARCNProgram>> GetAllProgram()
        {
            var Programs =  programRepository.FindAll();

            if (Programs == null)
                return ResponseModel<ARCNProgram>.ErrorMessage("Programs not found");

            return ResponseModel<ARCNProgram>.SuccessMessage(data: Programs);
        }
        public async ValueTask<ResponseModel<ARCNProgram>> GetProgramById(int Programid)
        {
            var Programs = await programRepository.FindByIdAsync(Programid);

            if (Programs == null)
                return ResponseModel<ARCNProgram>.ErrorMessage("Programs not found");

            return ResponseModel<ARCNProgram>.SuccessMessage(data: Programs);
        }
        public async ValueTask<ResponseModel<ARCNProgram>> UpdateProgramAsync(int Programid, ProgramDataModel model)
        {
            try
            {
                var Programs = await programRepository.FindByIdAsync(Programid);
                if (Programs != null)
                {
                    mapper.Map(model, Programs);

                    var res= programRepository.Update(Programs);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<ARCNProgram>
                    {
                        Success = true,
                        Message = "Updated successfully",
                        Data=res
                    };
                }
                else
                {
                    return new ResponseModel<ARCNProgram>
                    {
                        Success = false,
                        Message = "Update Failed",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResponseModel<ARCNProgram>
                {
                    Success = false,
                    Message = "Fail to insert",
                };
            }
        }

        public async ValueTask<ResponseModel<string>> DeleteProgramAsync(int Programid)
        {
            try
            {

                var Programs = await programRepository.FindByIdAsync(Programid);
                if (Programs != null)
                {
                    programRepository.Remove(Programs);
                    unitOfWork.SaveChanges();
                    return new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Program Deleted  successfully",
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
