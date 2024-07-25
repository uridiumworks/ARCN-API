using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Commons.Authorization
{
    public static class AppRoles
    {
        public const string SuperAdmin = nameof(SuperAdmin);
        public const string Staff = nameof(Staff);
        public const string Sales = nameof(Sales);
        public const string Tech = nameof(Tech);
        public const string DataEntry = nameof(DataEntry);

        public static IReadOnlyList<string> DefaultRoles { get; }
            = new ReadOnlyCollection<string>(new[]
            {
                SuperAdmin, Staff, Tech, Sales, DataEntry
            });

        public static bool IsDefault(string roleName)
            => DefaultRoles.Any(r => r == roleName);
    }
}
