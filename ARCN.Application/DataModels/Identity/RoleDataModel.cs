using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.DataModels.Identity
{
    public class RoleDataModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<RoleClaimDataModel> RoleClaims { get; set; } = new List<RoleClaimDataModel>();
    }
}
