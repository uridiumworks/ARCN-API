using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.TokenProviders
{
    public static class AppTokenProvider
    {
        public const string TotpProvider = "ARCNTokenProvider";
        public const string TwoFASmsProvider = "ARCN2faSmsProvider";
        public const string TwoFAEmailProvider = "ARCN2faEmailProvider";
        public const string TwoFAAuthenticatorAppProvider = "ARCN2faAuthenticatorAppProvider";
    }
}
