
using System.Net.Mime;
using ARCN.Application.DataModels.EmailData;
using ARCN.Application.DataModels.UserProfile;
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.RequestModel.EmailService;
using ARCN.Infrastructure.Services.RefitServices;


namespace ARCN.Infrastructure.Services.ApplicationServices
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<EmailService> logger;
        private readonly FluidParser fluidParser;
        private readonly IEmailSenderService emailSenderService;
        private readonly IExternalEmailService externalEmailService;
        private readonly UserManager<ApplicationUser> userManager;

        public EmailService(IConfiguration configuration,
            ILogger<EmailService> logger, FluidParser fluidParser,
            IEmailSenderService emailSenderService,
            UserManager<ApplicationUser> userManager, IExternalEmailService externalEmailService)

        {
            this.configuration = configuration;
            this.logger = logger;
            this.fluidParser = fluidParser;
            this.emailSenderService = emailSenderService;
            this.userManager = userManager;
            this.externalEmailService = externalEmailService;
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


        public void SendValidationCodeMail(ApplicationUser user, string code)
        {
            try
            {
                var emailData = new ChangePasswordEmailDataModel { Name = user.FirstName, OTP = code };

                string FilePath = Directory.GetCurrentDirectory() + "\\EmailFiles\\ChangePasswordValidation.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                var source = MailText;

                //var message = $"Your validation code is: {code}";
                //var bodyEmail = new EmailDataModel
                //{
                //    To = email,
                //    Text = message,
                //    Subject = "Nova bank - Verification Code",
                //    Html = message
                //};

                if (fluidParser.TryParse(source, out IFluidTemplate template, out var error))
                {
                    //var context = new TemplateContext(model);
                    TemplateOptions options = new TemplateOptions();
                    options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
                    var ctx = new TemplateContext(
                                       new { model = emailData }, options, true);

                    string output = template.Render(ctx);

                    var bodyEmail = new EmailRequestModel
                    {
                        To = user.Email,
                        Text = output,
                        Subject = "Nova Bank - Change Password",
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

                string FilePath = Directory.GetCurrentDirectory() + "\\EmailFiles\\ConfirmMail.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                var source = MailText;

                var data = new EmailConfirmationDataModel
                {
                    Name = user?.FirstName ?? "",
                    Email = user?.Email ?? "",
                    Token = token
                };

                if (fluidParser.TryParse(source, out IFluidTemplate template, out var error))
                {
                    TemplateOptions options = new TemplateOptions();
                    options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
                    var ctx = new TemplateContext(
                                       new { model = data }, options, true);

                    string output = template.Render(ctx);

                    var bodyEmail = new EmailDataModel
                    {
                        To = data.Email,
                        Text = output,
                        Subject = "Nova bank - Confirm your email",
                        Html = output
                    };

                    var multipart = new MultipartFormDataContent
                    {
                        { new StringContent(bodyEmail.To, Encoding.UTF8, MediaTypeNames.Text.Plain), "To" },
                        { new StringContent(bodyEmail.Text, Encoding.UTF8, MediaTypeNames.Text.Plain), "Text" },
                        { new StringContent(bodyEmail.Subject, Encoding.UTF8, MediaTypeNames.Text.Plain), "Subject" },
                        { new StringContent(bodyEmail.Html, Encoding.UTF8, MediaTypeNames.Text.Plain), "Html" }
                    };

                    var response = externalEmailService.SendMail(multipart);


                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
            }
        }


        public async ValueTask PasswordReset(ApplicationUser user, string otp)
        {
            try
            {



                string FilePath = Directory.GetCurrentDirectory() + "\\EmailFiles\\PasswordReset.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                var source = MailText;

                var mailData = new PasswordResetDataModel
                {
                    Name = user?.FirstName ?? "",
                    ReceiverEmail = user?.Email ?? "",
                    OTP = otp
                };

                if (fluidParser.TryParse(source, out IFluidTemplate template, out var error))
                {
                    TemplateOptions options = new TemplateOptions();
                    options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
                    var ctx = new TemplateContext(
                                       new { model = mailData }, options, true);

                    string output = template.Render(ctx);

                    var bodyEmail = new EmailDataModel
                    {
                        To = mailData.ReceiverEmail,
                        Text = output,
                        Subject = "Nova bank - Password Reset",
                        Html = output
                    };

                    var multipart = new MultipartFormDataContent
                    {
                        { new StringContent(bodyEmail.To, Encoding.UTF8, MediaTypeNames.Text.Plain), "To" },
                        { new StringContent(bodyEmail.Text, Encoding.UTF8, MediaTypeNames.Text.Plain), "Text" },
                        { new StringContent(bodyEmail.Subject, Encoding.UTF8, MediaTypeNames.Text.Plain), "Subject" },
                        { new StringContent(bodyEmail.Html, Encoding.UTF8, MediaTypeNames.Text.Plain), "Html" }
                    };

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
