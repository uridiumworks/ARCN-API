using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Commons.Authorization
{
    public static class AppRoleGroup
    {
        public const string SystemAccess = nameof(SystemAccess);
        public const string ManagementHierarchy = nameof(ManagementHierarchy);
    }
}
