using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Application.Interfaces.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;




namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class UserprofileService : IUserprofileService
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IMapper mapper;
        private readonly IValidator<ProfileUpdateDataModel> validatorProfile;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserIdentityService userIdentityService;


        public UserprofileService(
            IUserProfileRepository userProfileRepository,
            IMapper mapper,
            IValidator<ProfileUpdateDataModel> validatorProfile,
            IUnitOfWork unitOfWork,
            IUserIdentityService userIdentityService
            )
        {
            this.userProfileRepository = userProfileRepository;
            this.mapper = mapper;
            this.validatorProfile = validatorProfile;
            this.unitOfWork = unitOfWork;
            this.userIdentityService = userIdentityService;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            var profile = userProfileRepository.FindAll();
            return profile;
        }

        public IQueryable<ApplicationUser> GetById(string id)
        {
            var profile = userProfileRepository.FindAll()
                .Where(x => x.Id == id);
            // .Include(x=>x.SecurityQuestionAnswers);

            return profile;
        }

        public async ValueTask<ResponseModel<ApplicationUser>> GetProfile()
        {
            var profile = await userProfileRepository.FindByIdAsync(userIdentityService.UserId);

            if (profile == null) return new ResponseModel<ApplicationUser> { Message = "Profile not found", Success = false, Data = null, Errors = new List<string> { "Profile not found" } };

            return new ResponseModel<ApplicationUser> { Success = true, Message = "Success", Data = profile };
        }
        public async ValueTask<ApplicationUser> GetUserProfile()
        {
            var profile = await userProfileRepository.FindAll().Where(x => x.Id == userIdentityService.UserId)
                .FirstOrDefaultAsync();
            return profile;
        }

        public async ValueTask<ResponseModel<ApplicationUser>> UpdateUserProfile(int id, ProfileUpdateDataModel dataModel)
        {
            var validatorResult = await validatorProfile.ValidateAsync(dataModel);

            if (!validatorResult.IsValid)
            {
                var err = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ResponseModel<ApplicationUser> { Message = "Failed validation", Success = false, StatusCode=400, Errors = err };
            }

            var profile = await userProfileRepository.FindByIdAsync(id);
            if (profile == null) return new ResponseModel<ApplicationUser> { Message = "Profile not found", Success = false, StatusCode=404, Errors = new List<string> { "Profile not found" } };

            mapper.Map(dataModel, profile);

            userProfileRepository.Update(profile);
            await unitOfWork.SaveChangesAsync();

            return new ResponseModel<ApplicationUser> { Success = true, Message = "Success", Data = profile,StatusCode=200 };

        }



    }
}
