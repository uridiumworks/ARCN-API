using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Domain.Commons;

namespace ARCN.Domain.Entities
{
    public class Event:AuditableEntity
    {

        public int EventId { get; set; }
        public string? UserProfileId { get; set; }
        public string? Subject { get; set; }
        public string? BannerUrl { get; set; }
        public string? Description { get;set; }
        public string? AuthorName { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public TimeSpan? EventStartTime { get; set; }
        public TimeSpan? EventEndTime { get; set; }
        public string? DurationPerDay { get; set; }
        public string? Venue { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
