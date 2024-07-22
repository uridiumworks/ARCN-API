using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Infrastructure.RequestModel.EmailService
{
    public class EmailAttachment
    {
        public string FileName { get; set; }
        public byte[] Attachment { get; set; }

    }
}
