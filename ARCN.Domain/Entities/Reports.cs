﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Domain.Commons;

namespace ARCN.Domain.Entities
{
    public class Reports:AuditableEntity
    {

        public int ReportsId { get; set; }
        public string? UserProfileId { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? BannerUrl { get; set; }
        public string? Description { get;set; }
        public string? AuthorName { get; set; }
        public string? AuthorEmail { get; set; }
        public string? AuthorPhoneNumber { get; set; }
        public string? Visibility { get; set; }
        public bool? UseBanner { get; set; }
        public DateTime? PublishDate { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
