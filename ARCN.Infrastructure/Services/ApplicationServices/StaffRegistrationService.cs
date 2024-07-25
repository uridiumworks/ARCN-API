using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces.Repositories;
using ARCN.Domain.Commons.Authorization;
using ARCN.Infrastructure.Services.ApplicationServices;
using ARCN.Repository.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Errors = ARCN.Application.Exceptions;

namespace ARCN.Application.Interfaces.Services
{
    public class StaffRegistrationService : IStaffRegistrationService
    {
        private readonly IValidator<NewUserDataModel> validatorNewUser;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly ILogger<StaffRegistrationService> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public StaffRegistrationService(
            IValidator<NewUserDataModel> validatorNewUser,
             IUserProfileRepository userProfileRepository,
             ILogger<StaffRegistrationService> logger,
             UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager)
        {
            this.validatorNewUser = validatorNewUser;
            this.userProfileRepository = userProfileRepository;
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

       

    }
}
