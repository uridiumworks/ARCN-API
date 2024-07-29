using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Domain.Commons;

namespace ARCN.Domain.Entities
{
    public class Naris:AuditableEntity
    {

        public int NarisId { get; set; }
        public string? UserProfileId { get; set; }
        public string? InstitutionName { get; set; }
        public string? Description { get;set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public int? StateId { get; set; }
        public int? LocalGovernmentAreaId { get; set; }
        public DateTime? EstablishDate { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? LogoUrl { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual State? States { get; set; }
        public virtual LocalGovernmentArea? LocalGovernmentArea { get; set; }
    }
}
