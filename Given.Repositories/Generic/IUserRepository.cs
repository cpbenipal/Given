using Given.DataContext.Entities;
using Given.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface IUserRepository
    {
        Task<UserModel> Authenticate(string email, string password);
        // Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<ListUserModel> GetUserByIdAsync(Guid id);
        Task<string> Create(RegisterModel user, string password);
        Task<bool> ConfirmCompany(ConfirmEmailModel model);
        Task<string> Update(UpdateModel user, string password = null);
        //  Task Delete(Guid id);        
        Task<bool> ForgotPassword(ForgotPasswordViewModel model);
        Task<bool> ResetPassword(NewPasswordViewModel model);
        //  Task<bool> VerifyUser(string email, string otp, Guid? InvitedBy);
        Task<string> InviteUser(InviteViewModel model);
        Task<List<ListUserModel>> GetAllUsersByAdminAsync(Guid UserId);
        Task<List<ListUserModel>> GetAllUsersBySuperAdminAsync(Guid CompanyId);
        Task<string> UploadPic(ProfilePicModel model);
        Task<bool> ChangePassword(ChangePasswordViewModel model);
        Task<byte[]> GetProfilePicAsync(Guid id);
        Task<string> AcceptInvite(UserRegisterModel model, string password);       
        Task<bool> VerifyUser(string email, string otp, Guid? invitedBy);
        Task<string> IsEmailConfirmed(string email);
    }
}