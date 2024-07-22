using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ARCN.Infrastructure.RequestModel.EmailService
{
    public class EmailRequestModel
    {
        [JsonPropertyName("to")]
        public string To { get; set; }
        [JsonPropertyName("bcc")]
        public string Bcc { get; set; }
        [JsonPropertyName("cc")]
        public string Cc { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }
        [JsonPropertyName("attachment")]
        public List<string> Attachments { get; set; } = new();
        public List<EmailAttachment> FileAttachments { get; set; } = new();
    }
}
