﻿using ARCN.Application.Interfaces.Services;

namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string UserName => GetUserName();

        public string? UserProfileId => GetUserProfileId();
        public string? AccountNumber => GetUserAccountNumber();

        public string? UserId => GetSubjectId();

        public string Email => GetEmail();

        private string GetUserName()
        {
            var identity = httpContextAccessor?.HttpContext?.User?.Identity;
            var id = identity as ClaimsIdentity;
            var claim = id?.FindFirst("firstName");

            if (claim == null) throw new InvalidOperationException($"{nameof(ClaimTypes.Email)} claim is missing");
            return claim.Value;
        }

        private string GetEmail()
        {
            var identity = httpContextAccessor?.HttpContext?.User?.Identity;
            var id = identity as ClaimsIdentity;
            var claim = id?.FindFirst(ClaimTypes.Email);

            if (claim == null) throw new InvalidOperationException($"{nameof(ClaimTypes.Email)} claim is missing");
            return claim.Value;
        }

        private string GetSubjectId()
        {
            var identity = httpContextAccessor?.HttpContext?.User?.Identity;
            var id = identity as ClaimsIdentity;
            var claim = id?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null) throw new InvalidOperationException($"{nameof(ClaimTypes.NameIdentifier)} claim is missing");
            return claim.Value;
        }

        private string GetUserProfileId()
        {
            var identity = httpContextAccessor?.HttpContext?.User?.Identity;
            var id = identity as ClaimsIdentity;
            var claim = id?.FindFirst("profileId");

            if (claim == null) throw new InvalidOperationException("UserProfileId claim is missing");
            return claim.Value;
        }

        private string GetUserAccountNumber()
        {
            var identity = httpContextAccessor?.HttpContext?.User?.Identity;
            var id = identity as ClaimsIdentity;
            var claim = id?.FindFirst("acctNumber");

            if (claim == null) throw new InvalidOperationException("AccountNumber claim is missing");
            return claim.Value;
        }
    }
}