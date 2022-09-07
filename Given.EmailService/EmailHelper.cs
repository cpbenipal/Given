using System;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace Given.EmailService
{
    public class EmailHelper : IEmailHelper
    {
        readonly IConfiguration _configuration;
        readonly string BaseUrl;
        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            var APISettings = _configuration.GetSection("APISettings");
            BaseUrl = APISettings["BaseUrl"];
        }
        // Company SignUp Notification
        public void SendEmailToCompany(string firstName, string email, string otp, Guid CompanyId)
        {
            string bodytext = GetMessage(firstName, email, otp, CompanyId);
            string subject = "Confirm your Company account on Given";
            SendEmail(email, subject, bodytext);
        }

        private string GetMessage(string firstName, string email, string otp, Guid companyId)
        {
            return $@"<p> Hi {firstName}, </p>  
                    <p> Please enter email and OTP to confirm company account on 'Given': {BaseUrl}authentication/confirm/{companyId}  </p> 
                    <p> Email: {email} </p>
                    <p> OTP: {otp} </p>                         
                    The Given Team";
        }

        public void SendOTPToEmail(string firstname, string receiver, string otp)
        {
            string bodytext = GetMessage(firstname, receiver, otp);
            string subject = "Confirm your Email on Given";
            SendEmail(receiver, subject, bodytext);
        }
        public void SendConfirmationEmail(string FirstName, string receiver, string company, string roleName)
        {
            string bodytext = GetMessageEmail(FirstName, receiver, company, roleName);
            string subject = $@"Access to {company} company was granted";
            SendEmail(receiver, subject, bodytext);
        }

        private void SendEmail(string receiver, string subject, string bodytext)
        {
            var EmailHelperModel = _configuration.GetSection("EmailHelperModel");

            var smtpProvider = EmailHelperModel["EmailProvider"];
            var portNumber = Convert.ToInt32(EmailHelperModel["PortNumber"]);
            var user = EmailHelperModel["User"];
            var password = EmailHelperModel["Password"];
            var sender = EmailHelperModel["EmailFrom"];

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", sender));
            emailMessage.To.Add(new MailboxAddress("", receiver));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("html")
            {
                Text = bodytext
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(smtpProvider, portNumber, SecureSocketOptions.Auto);
                client.Authenticate(user, password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
        private string GetMessage(string firstname, string receiver, string otp)
        {
            return $@"<p> Hi {firstname}, </p>  
                    <p> Please enter OTP to confirm Email on 'Given': {BaseUrl}authentication/confirm  </p> 
                    <p> Email: {receiver} </p>
                    <p> OTP: {otp} </p>                         
                    The Given Team";
        }
        private string GetMessageEmail(string firstName, string receiver, string company, string rolename)
        {
            return $@"<p> Dear {firstName}, </p>  
                    <p> You have been granted {rolename} access to the {company} company </p> 
                    <p> Login here : {BaseUrl}authentication/login </p>   
                    <p> Forgot Password ? : {BaseUrl}authentication/forgot-password </p> 
                    The Given Team";
        }
        //private void GetNotificationForAdmin(string adminemail, string firstname, string receiver, string body)   
        //{
        //    string message =  $@"<p> Hi {firstname}, </p>  
        //            <p> Given notification sent to {receiver} </p> 
        //            <p> Here's the detail </p>
        //            <p><br/>{body}</p>                     
        //            The Given Team";

        //    SendEmail(adminemail, "Notification sent to Invitee!", message);
        //}


        public void SendConfirmationResetPassword(string firstName, string email)
        {
            string bodytext = GetResetMessage(firstName, email);
            string subject = "You'hv successfully reset password on Given";
            SendEmail(email, subject, bodytext);
        }

        private string GetResetMessage(string firstName, string receiver)
        {
            return $@"<p> Dear {firstName}, </p>  
                    <p> You'hv successfully reset password on Given </p> 
                    <p> Login here : {BaseUrl}authentication/login </p>                        
                    The Given Team";
        }
        private string GetInviteMessage(string receiverName, string Company, string sendName, Guid InvitedBy, Guid InvitedTo, string otp, string inviteerole)
        {
            return $@"<p> Dear {receiverName}, </p>  
                    <p>You have invited by {sendName} to join company {Company} for {inviteerole} role </p>                     
                    <p>Accept invitation : {BaseUrl}authentication/acceptinvitation/{InvitedBy}/{InvitedTo} </p>                        
                    <p>Enter OTP: {otp} </p>
                    The Given Team";
        }

        private string GetMessageforFPwd(string email, string otp)
        {
            return $@"<p> Dear user, </p>  
                    <p> Enter your email and otp to set new password </p>                     
                    <p>Enter OTP: {otp} </p>                    
                    <p> Forgot Password ? : {BaseUrl}authentication/confirm-and-update-password </p> 
                    The Given Team";
        }

        public void SendInvite(string receiverEmail, string receiverName, string otp, Guid currentUserId, Guid InvitedBy, string Company, string owner, string inviteerole)
        {
            string bodytext = GetInviteMessage(receiverName, Company, owner, currentUserId, InvitedBy, otp, inviteerole);
            string subject = $@"Invitation to join the {Company} company on Given";
            SendEmail(receiverEmail, subject, bodytext);
        }
        private string GetAccountUpdateMessage(string firstName)
        {
            return $@"<p> Dear {firstName}, </p>  
                    <p> You'hv successfully updated your account on Given </p>                              
                    The Given Team";
        }
        public void SendAccountUpdateEmail(string firstName, string email)
        {
            string bodytext = GetAccountUpdateMessage(firstName);
            string subject = $@"Notification| Account Update on Given";
            SendEmail(email, subject, bodytext);
        }
        public void SendForgotPwdEmail(string email, string Otp)
        {
            string bodytext = GetMessageforFPwd(email, Otp);
            string subject = $@"Forgot password for Given";
            SendEmail(email, subject, bodytext);
        }

        public void SendConfirmationToCompany(string firstName, string email)
        {
            string bodytext = GetMessageForCompany(firstName, email);
            string subject = $@"Company account has been created on Given";
            SendEmail(email, subject, bodytext);
        }

        private string GetMessageForCompany(string firstName, string email)
        {
            return $@"<p> Dear {firstName}, </p>  
                    <p> You'hv successfully updated your account on Given </p>  
                    <p> Login here : {BaseUrl}authentication/login </p>   
                    <p> Forgot Password ? : {BaseUrl}authentication/forgot-password </p> 
                    The Given Team";
        }
    }
}
