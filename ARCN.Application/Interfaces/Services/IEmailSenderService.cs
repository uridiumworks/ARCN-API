using System.Threading.Tasks;
using ARCN.Application.DataModels.EmailData;


namespace ARCN.Application.Interfaces.Services
{
    public interface IEmailSenderService
    {
        string SendEMail(EmailDataModel mailRequest);
        ValueTask<string> SendEmailAsync(EmailDataModel mailRequest, SmtpSettings? customSmtpSettings = null);

    }
}
