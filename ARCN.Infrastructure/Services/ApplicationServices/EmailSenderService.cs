
using System.Net.Mime;
using ARCN.Application.DataModels.EmailData;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.RequestModel.EmailService;
using ARCN.Infrastructure.Services.RefitServices;


namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class EmailSenderService : IEmailSenderService
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<EmailSenderService> logger;
        private readonly FluidParser fluidParser;
        private readonly IExternalEmailService externalEmailService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> role;
        public EmailSenderService(IConfiguration configuration,
            ILogger<EmailSenderService> logger, FluidParser fluidParser,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> role,
        IExternalEmailService externalEmailService)

        {
            this.configuration = configuration;
            this.logger = logger;
            this.fluidParser = fluidParser;
            this.userManager = userManager;
            this.externalEmailService = externalEmailService;
            this.role = role;
        }

        public async ValueTask ForgotPasswordMail(ForgotPasswordMail forgotPasswordMail)
        {

            var combineToken = configuration["ForgotPasswordCallback"] + $"?email={forgotPasswordMail.Email}&token={forgotPasswordMail.Token}";

            forgotPasswordMail.Url = combineToken;

            string FilePath = Directory.GetCurrentDirectory() + "\\EmailFiles\\ForgotPassword.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            var source = MailText;

            try
            {
                if (fluidParser.TryParse(source, out IFluidTemplate template, out var error))
                {
                    //var context = new TemplateContext(model);
                    TemplateOptions options = new TemplateOptions();
                    options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
                    var ctx = new TemplateContext(
                                       new { model = forgotPasswordMail }, options, true);

                    string output = template.Render(ctx);

                    var bodyEmail = new EmailRequestModel
                    {
                        To = forgotPasswordMail.Email,
                        Text = output,
                        Subject = "Reset your password",
                        Html = output
                    };

                    var multipart = new MultipartFormDataContent();
                    multipart.Add(new StringContent(bodyEmail.To, Encoding.UTF8, MediaTypeNames.Text.Plain), "To");
                    multipart.Add(new StringContent(bodyEmail.Text, Encoding.UTF8, MediaTypeNames.Text.Plain), "Text");
                    multipart.Add(new StringContent(bodyEmail.Subject, Encoding.UTF8, MediaTypeNames.Text.Plain), "Subject");
                    multipart.Add(new StringContent(bodyEmail.Html, Encoding.UTF8, MediaTypeNames.Text.Plain), "Html");

                    var response = externalEmailService.SendMail(multipart);


                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }
        }


        public async ValueTask ConfirmEmailAddress(string token, ApplicationUser user)
        {
            try
            {
                string combineToken = string.Empty;
                if (user.IsAdmin)
                {
                    combineToken = configuration["AdmEmailConfirm"] + $"?email={user.Email}&token={token}";
                }
                else
                {
                    combineToken = configuration["EmailConfirm"] + $"?email={user.Email}&token={token}";
                }


                string FilePath = Directory.GetCurrentDirectory() + "\\EmailFiles\\ConfirmationEmail.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                var source = MailText;

                var data = new EmailConfirmationDataModel
                {
                    Name = user != null ? $"{user?.FirstName}  {user?.LastName}" : $"{user?.Email}",
                    Email = user.Email,
                    Token = combineToken
                };

                if (fluidParser.TryParse(source, out IFluidTemplate template, out var error))
                {
                    //var context = new TemplateContext(model);
                    TemplateOptions options = new TemplateOptions();
                    options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
                    var ctx = new TemplateContext(
                                       new { model = data }, options, true);

                    string output = template.Render(ctx);

                    var bodyEmail = new EmailDataModel
                    {
                        To = data.Email,
                        Text = output,
                        Subject = "ARCN - Confirm your email",
                        Html = output
                    };

                    var multipart = new MultipartFormDataContent
                    {
                        { new StringContent(bodyEmail.To, Encoding.UTF8, MediaTypeNames.Text.Plain), "To" },
                        { new StringContent(bodyEmail.Text, Encoding.UTF8, MediaTypeNames.Text.Plain), "Text" },
                        { new StringContent(bodyEmail.Subject, Encoding.UTF8, MediaTypeNames.Text.Plain), "Subject" },
                        { new StringContent(bodyEmail.Html, Encoding.UTF8, MediaTypeNames.Text.Plain), "Html" }
                    };

                    //var response = emailSenderService.SendEMail(bodyEmail);
                    var response = externalEmailService.SendMail(multipart);


                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }
        }


    }

}
