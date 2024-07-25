using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Domain.Commons.Authorization
{
    public static class AppAction
    {
        public const string Read = nameof(Read);
        public const string Create = nameof(Create);
        public const string Update = nameof(Update);
        public const string Delete = nameof(Delete);
        public const string Upload = nameof(Upload);
        public const string Unlock = nameof(Unlock);
        public const string Reset = nameof(Reset);
        public const string Manage = nameof(Manage);

    }
}
