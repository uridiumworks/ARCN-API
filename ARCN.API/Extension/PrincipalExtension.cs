using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace ARCN.API.Extension
{
    public static class PrincipalExtension
    {
        /// <summary>
        /// Gets the subject identifier.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string GetSubjectId(this IPrincipal principal)
        {
            return principal.Identity.GetSubjectId();
        }

        /// <summary>
        /// Gets the subject identifier.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">sub claim is missing</exception>
        [DebuggerStepThrough]
        public static string GetSubjectId(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null) throw new InvalidOperationException($"{ClaimTypes.NameIdentifier} claim is missing");
            return claim.Value;
        }

        public static string GetEmail(this IPrincipal principal)
        {
            return principal.Identity.GetEmail();
        }

        private static string GetEmail(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindFirst(ClaimTypes.Email);

            if (claim == null) throw new InvalidOperationException($"{nameof(ClaimTypes.Email)} claim is missing");
            return claim.Value;
        }

        public static string GetUserName(this IPrincipal principal)
        {
            return principal.Identity.GetUserName();
        }
        private static string GetUserName(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindFirst(ClaimTypes.Name);

            if (claim == null) throw new InvalidOperationException($"{nameof(ClaimTypes.Email)} claim is missing");
            return claim.Value;
        }

        public static string GetPatientProfile(this IPrincipal principal)
        {
            return principal.Identity.GetPatientProfile();
        }
        private static string GetPatientProfile(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindFirst("profileId");

            if (claim == null) throw new InvalidOperationException($"profileId claim is missing");
            return claim.Value;
        }
        public static IEnumerable<Claim> GetUserRole(this IPrincipal principal)
        {
            return principal.Identity.GetUserRole();
        }
        private static IEnumerable<Claim> GetUserRole(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindAll(ClaimTypes.Role);

            if (claim == null) throw new InvalidOperationException($"roleId claim is missing");
            return claim;
        }




    }
}
