
using Microsoft.AspNetCore.Http;

namespace ARCN.Application.DataModels.EmailData
{
    public class EmailDataModel
    {
        public string To { get; set; }
        public string? Bcc { get; set; }
        public string? Cc { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
        public string? MultipartFormDataContent { get; set; }
        public IFormFile? Attachments { get; set; }
        public byte[] Attachmentss { get; set; }
        public List<EmailAttachment> FileAttachments { get; set; } = new();
    }
    public class PasswordResetDataModel
    {
        public string Name { get; set; }
        public string OTP { get; set; }
        public string ReceiverEmail { get; set; }
    }
}
