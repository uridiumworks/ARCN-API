namespace ARCN.Infrastructure.Services.RefitServices
{
    public interface IExternalEmailService
    {
        [Post("/EmailSender/SendMultipleMail")]
        Task<HttpResponseMessage> SendMail(MultipartFormDataContent model);
    }
}

