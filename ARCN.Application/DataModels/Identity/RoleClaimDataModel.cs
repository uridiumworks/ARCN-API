using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.DataModels.Identity
{
    public class RoleClaimDataModel
    {
        public string? RoleId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public string? Description { get; set; }
        public string? Group { get; set; }
        public bool IsAssignedToRole { get; set; }
    }
}
