using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Domain.Commons;

namespace ARCN.Domain.Entities
{
    public class ARCNProgram : AuditableEntity
    {

        public int ARCNProgramId { get; set; }
        public string? UserProfileId { get; set; }
        public string? Subject { get; set; }
        public string? BannerUrl { get; set; }
        public string? Description { get; set; }
        public string? Venue { get; set; }
        public string? Author { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public string? DurationPerDay { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
