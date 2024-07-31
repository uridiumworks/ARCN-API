
namespace NovaBank.API.Extensions
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

        public static string GetUserProfile(this IPrincipal principal)
        {
            return principal.Identity.GetUserProfile();
        }
        private static string GetUserProfile(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindFirst("profileId");

            if (claim == null) throw new InvalidOperationException($"profileId claim is missing");
            return claim.Value;
        }




    }
}
