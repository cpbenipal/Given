using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Given.Models
{
    public class CompanySizeModel
    {
        public Guid Id { get; set; }
        public string Size { get; set; }
        public short? DisplayOrder { get; set; }
    }
    public class RoleModel
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class ConfirmModel
    {                                        
        public string Email { get; set; }
        public string OTP { get; set; }
    }
    public class ConfirmEmailModel
    {
        public Guid CompanyId { get; set; }    
        public string Email { get; set; }
        public string OTP { get; set; }
        public string Password { get; set; }
    }
    public class ProfilePicModel
    {
        public Guid Id { get; set; }
        public byte[] Photo { get; set; }
    }
    public class UpdateModel
    {        
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        //[Required]
        //public string Password { get; set; }
        public string PhoneNumber { get; set; }  
        //[Required]
        //public string CompanyName { get; set; }
        //[Required]
        //public Guid CompanySizeId { get; set; }
        //public Guid CompanyId { get; set; }
        //[Required]
        public ICollection<UserRoleModel> UserRole { get; set; } 
        //public byte[] Photo { get; set; }
    }
     
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid CompanyId { get; set; }
        public string Company { get; set; }    
        public string Email { get; set; }
        public string Role { get; set; }   
        public Guid RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Photo { get; set; }
        public string Token { get; set; }      
    }
    public class UserRoleModel
    {            
        public Guid? RoleId { get; set; }  
    }
    public class AuthenticateModel
    {
        [Required(ErrorMessage = "E-mail is required.")]
        [MaxLength(256, ErrorMessage = "E-mail cannot be longer than 200 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(256, ErrorMessage = "Password cannot be longer than 50 characters.")]
        public string Password { get; set; }
    }
    public class ResponseUserModel
    {
        public Guid UserUuid { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool UserType { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public byte[] Photo { get; set; }
    }
    public class RegisterModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }         
        [Required]
        public Guid CompanySizeId { get; set; }
        [Required]
        public string FirstName { get; set; }
    }
    public class UserRegisterModel
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Otp { get; set; }
        public Guid? InvitedBy { get; set; }
    }
    public class ApiResult
    {
        public int ReturnCode { get; set; }
        public object ReturnMessage { get; set; }
        public object ReturnData { get; set; }
        public bool ReturnStatus { get; set; }
        public ApiResult()
        {
            ReturnCode = 0;
            ReturnMessage = null;
            ReturnData = null;
            ReturnStatus = false;
        }
        //public ApiResult(int _ReturnCode, object _ReturnMessage, object _ReturnData)
        //{
        //    ReturnCode = _ReturnCode;
        //    ReturnMessage = _ReturnMessage;
        //    ReturnData = _ReturnData;
        //}
    }

    public class CommonMethods   
    {
        public static ApiResult CommonAPIResult(string ReturnData, int ReturnCode, string ReturnMessage, bool ReturnStatus)
        {
            ApiResult apiResult = new ApiResult();
            apiResult.ReturnData = ReturnData;
            apiResult.ReturnCode = ReturnCode;
            apiResult.ReturnMessage = ReturnMessage;
            apiResult.ReturnStatus = ReturnStatus;

            return apiResult;
        }
    }
    public class ListUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }      
        public bool? EmailConfirmed { get; set; }
        public DateTime? EmailConfirmedOn { get; set; }
        public string PhoneNumber { get; set; }    
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public byte[] Photo { get; set; }
        public Guid? InvitedBy { get; set; }
        public DateTime? InvitedOn { get; set; }
        public string Company { get; set; }
        public Guid CompanyId { get; set; }    
        public Guid? CompanySizeId { get; set; }  
        public string CompanySize { get; set; }    
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public class APISettings
    {
        public string BaseUrl { get; set; }
    }
    public class InviteViewModel
    {
        public Guid InvitedBy { get; set; }    
        public string ReceiverName { get; set; } 
        public string ReceiverEmail { get; set; }
        public Guid ReceiverRoleId { get; set; }   
    }
    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
    }
    public class NewPasswordViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }        
    }
    public class ChangePasswordViewModel
    {                  
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class KPIModel
    {
        public int? TotalContacts { get; set; }
        public int? TotalGifts { get; set; }
        public decimal? TotalGiftMoney { get; set; }
    }
}
