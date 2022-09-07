using System;

namespace Given.EmailService
{
    public interface IEmailHelper
    {
        void SendEmailToCompany(string firstName, string email, string otp, Guid CompanyId);
        void SendOTPToEmail(string firstname, string receiver, string otp);
        void SendConfirmationEmail(string FirstName, string receiver, string company, string RoleName); 
        void SendConfirmationResetPassword(string firstName, string email);
        void SendInvite(string receiverEmail, string receiverName, string otp, Guid InvitedBy, Guid InvitedTo , string Company , string owner, string inviteerole); 
        void SendAccountUpdateEmail(string firstName, string email);
        void SendForgotPwdEmail(string email, string Otp);
        void SendConfirmationToCompany(string firstName, string email);
    }
}
