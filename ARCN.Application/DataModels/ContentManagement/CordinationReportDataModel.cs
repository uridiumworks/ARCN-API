using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Domain.Commons;

namespace ARCN.Application.DataModels.ContentManagement
{
    public class CordinationReportDataModel
    {
        public string? Title { get; set; }
        public string? BannerUrl { get; set; }
        public string? Description { get; set; }
        public string? PublisherName { get; set; }
        public string? AuthorEmail { get; set; }
        public DateTime? PublishDate { get; set; }

    }
}
