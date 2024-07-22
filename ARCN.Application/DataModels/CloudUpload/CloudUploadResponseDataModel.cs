using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.DataModels.CloudUpload
{
    public class CloudUploadResponseDataModel
    {
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public string? Status { get; set; }
        public Stream? Content { get; set; }
    }
}
