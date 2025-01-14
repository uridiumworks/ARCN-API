﻿using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces
{
    public interface IEmailSenderService
    {
        ValueTask ForgotPasswordMail(ForgotPasswordMail forgotPasswordMail);
        ValueTask ConfirmEmailAddress(string token, ApplicationUser user);


    }
}
