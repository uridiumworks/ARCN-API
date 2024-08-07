using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Domain.Commons;

namespace ARCN.Domain.Entities
{
    public class SupervisionReport : AuditableEntity
    {

        public int SupervisionReportId { get; set; }
        public string? UserProfileId { get; set; }
        public string? Title { get; set; }
        public string? BannerUrl { get; set; }
        public string? Description { get; set; }
        public string? PublisherName { get; set; }
        public string? AuthorEmail { get; set; }
        public DateTime? PublishOn { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
