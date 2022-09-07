using AutoMapper;
using Microsoft.Extensions.Options;
using Given.DataContext.Entities;
using Given.Models;
using Given.Repositories.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Given.Models.Helpers;
using System;
using Given.DataContext.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Given.EmailService;

namespace Given.Repositories
{
    public class UserRepository : RepositoryBase<DBGIVENContext, User, UserModel>, IUserRepository
    {
        private readonly AppSettings _appSettings;
        private IMapper _mapper;
        private IEmailHelper _emailHelper;
        public UserRepository(DBGIVENContext dbContext, IMapper mapper, IOptions<AppSettings> appSettings, IEmailHelper emailHelper) : base(dbContext, mapper)
        {
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _emailHelper = emailHelper;
        }

        public async Task<string> IsEmailConfirmed(string email)
        {
            var user = await GetWithoutMapping(x => x.Email == email).FirstOrDefaultAsync();

            if (user == null)
                return "404";   // email not found
            else if (user.EmailConfirmed == false)
                return "401";   //   email not confirmed
            else
                return string.Empty;  // ok
        }

        public async Task<UserModel> Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = (from f in databaseContext.User
                        join cm in databaseContext.Company on f.CompanyId equals cm.CompanyId
                        join ur in databaseContext.UserRole on f.Id equals ur.UserId
                        join r in databaseContext.Role on ur.RoleId equals r.RoleId
                        where f.Email == email && f.EmailConfirmed == true
                        select new
                        {
                            f.Id,
                            f.PasswordHash,
                            f.PasswordSalt,
                            f.FirstName,
                            f.LastName,
                            f.PhoneNumber,
                            f.Photo,
                            f.Email,
                            Company = cm.CompanyName,
                            cm.CompanyId,
                            r.RoleId,
                            Role = r.RoleName
                        }).FirstOrDefault();


            // check if email exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var model = new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                Company = user.Company,
                Email = user.Email,
                Role = user.Role,
                RoleId = user.RoleId,
                PhoneNumber = user.PhoneNumber,
                Photo = user.Photo
            };

            model.Token = tokenHandler.WriteToken(token);
            //model.Photo = null;
            // authentication successful
            return model;
        }


        public async Task<bool> VerifyUser(string email, string otp, Guid? InvitedBy)
        {
            return await Get(u => u.Email.Equals(email) && u.Otp.Equals(otp) && u.InvitedBy.Equals(InvitedBy) && u.EmailConfirmed == false).AnyAsync();
        }

        public async Task<ListUserModel> GetUserByIdAsync(Guid id)
        {
            return (from u in databaseContext.User
                    join ur in databaseContext.UserRole on u.Id equals ur.UserId
                    join r in databaseContext.Role on ur.RoleId equals r.RoleId
                    join o in databaseContext.Company on u.CompanyId equals o.CompanyId
                    join st in databaseContext.CompanySize on o.CompanySizeId equals st.Id into gj
                    from subpet in gj.DefaultIfEmpty()
                    where (u.Id == id)
                    select new ListUserModel
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        EmailConfirmed = u.EmailConfirmed,
                        EmailConfirmedOn = u.EmailConfirmedOn,
                        PhoneNumber = u.PhoneNumber,
                        CreatedOn = u.CreatedOn,
                        UpdatedOn = u.UpdatedOn,
                        UpdatedBy = u.UpdatedBy,
                        Photo = u.Photo,
                        InvitedBy = u.InvitedBy,
                        InvitedOn = u.InvitedOn,
                        Company = o.CompanyName,
                        CompanySizeId = o.CompanySizeId ?? null,
                        CompanySize = subpet.Size ?? null,
                        RoleId = r.RoleId,
                        RoleName = r.RoleName,
                        CompanyId = o.CompanyId
                    }).OrderBy(xx => xx.CreatedOn).FirstOrDefault();
        }

        public async Task<string> Create(RegisterModel user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                // throw new AppException("Password is required");
                return "404";

            if (GetAll().Any(x => x.Email == user.Email))
                return "412";
            // throw new AppException("Email \"" + user.Email + "\" is already taken");

            try
            {

                var userdetail = new User();
                userdetail.Id = Guid.NewGuid();
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                var superAdminRoleId = await databaseContext.Role.FirstOrDefaultAsync(x => x.RoleName == "SuperAdmin");
                var role = new UserRole { UserId = userdetail.Id, RoleId = superAdminRoleId.RoleId };
                userdetail.PasswordHash = passwordHash;
                userdetail.PasswordSalt = passwordSalt;
                userdetail.EmailConfirmed = false;
                userdetail.EmailConfirmedOn = DateTime.Now;
                userdetail.Otp = GenerateOTP();
                userdetail.UpdatedOn = DateTime.Now;
                userdetail.UpdatedBy = userdetail.Id;
                userdetail.FirstName = "Super";
                userdetail.LastName = "Admin";
                userdetail.Email = user.Email;
                userdetail.PhoneNumber = user.PhoneNumber;
                // Add to user role
                userdetail.UserRole.Add(role);
                Guid CompanyId = Guid.NewGuid();
                // Add company                
                userdetail.Company = new Company { CompanyId = CompanyId, CompanyName = user.Company, CompanySizeId = user.CompanySizeId };
                AddWithoutMapping(userdetail);
                await SaveChangesAsync();


                // databaseContext.Set<UserRole>().Add(role);
                //  await SaveChangesAsync();

                // Send otp to email to confirm account 
                _emailHelper.SendEmailToCompany(userdetail.FirstName, userdetail.Email, userdetail.Otp, CompanyId);
                return "200";
            }
            catch
            {
                return "400";
            }
        }

        private static string GenerateOTP()
        {
            return Guid.NewGuid().ToString().Substring(0, 6);
        }

        public async Task<bool> ConfirmCompany(ConfirmEmailModel model)
        {
            bool StatuseCode = false;
            var user = await GetWithoutMapping(u => u.Email.Equals(model.Email) && u.Otp.Equals(model.OTP) && u.CompanyId.Equals(model.CompanyId)).FirstOrDefaultAsync();
            if (user == null)
                // throw new AppException("Company information does not match!");
                StatuseCode = false;
            else
            {
                // check if password is correct
                if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                    //  throw new AppException("Password is Incorret!");
                    StatuseCode = false;
                else
                {
                    user.EmailConfirmed = true;
                    user.Otp = null;
                    user.UpdatedOn = DateTime.Now;
                    UpdateWithoutMapping(user);
                    await SaveChangesAsync();

                    // Send otp to email to confirm account
                    _emailHelper.SendConfirmationToCompany(user.FirstName, user.Email);
                    StatuseCode = true;
                }
            }
            return StatuseCode;
        }

        // Invite Administrator or User
        public async Task<string> InviteUser(InviteViewModel model)
        {
            try
            {
                if (!GetAll().Any(x => x.Email == model.ReceiverEmail))
                {
                    var inviter = await GetUserByIdAsync(model.InvitedBy);
                    Guid userId = Guid.NewGuid();
                    var adminuser =
                        new User
                        {
                            Email = model.ReceiverEmail,
                            EmailConfirmed = false,
                            CompanyId = inviter.CompanyId,
                            CreatedOn = DateTime.Now,
                            UpdatedOn = DateTime.Now,
                            UpdatedBy = model.InvitedBy,
                            InvitedBy = model.InvitedBy,
                            Id = userId,
                            InvitedOn = DateTime.Now,
                            Otp = GenerateOTP()
                        };

                    adminuser.UserRole.Add(new UserRole { UserId = userId, RoleId = model.ReceiverRoleId });
                    AddWithoutMapping(adminuser);
                    await SaveChangesAsync();

                    // Get role name for which invitee invited for 
                    var rolename = databaseContext.Role.FirstOrDefault(x => x.RoleId == model.ReceiverRoleId);

                    // Send invitation email
                    _emailHelper.SendInvite(model.ReceiverEmail, model.ReceiverName, adminuser.Otp, model.InvitedBy, userId,
                        inviter.Company, inviter.FirstName, rolename.RoleName);

                    return "200";
                }
                else
                {
                    return "404";
                }
            }
            catch
            {
                return "400";
            }
        }

        public async Task<string> Update(UpdateModel userParam, string password = null)
        {
            var user = await GetWithoutMapping(u => u.Id.Equals(userParam.Id)).FirstOrDefaultAsync();

            if (user == null)
                return "404";
            else
            {
                user.FirstName = userParam.FirstName;
                user.LastName = userParam.LastName;
                user.PhoneNumber = userParam.PhoneNumber;
                user.UpdatedOn = DateTime.Now;
                UpdateWithoutMapping(user);
                await SaveChangesAsync();

                if (userParam.UserRole != null)
                {
                    // remove existing user role
                    var userroles = databaseContext.UserRole.Where(x => x.UserId == userParam.Id);
                    databaseContext.UserRole.RemoveRange(userroles);
                    await SaveChangesAsync();

                    var newRoles = userParam.UserRole.Select(x => new UserRole { Id = Guid.NewGuid(), RoleId = x.RoleId, UserId = userParam.Id });
                    databaseContext.UserRole.AddRange(newRoles);
                    await SaveChangesAsync();
                }
                // Send account updated notification
                _emailHelper.SendAccountUpdateEmail(user.FirstName, user.Email);
                return "200";
            }
        }

        public async Task Delete(Guid id)
        {
            var user = await GetWithoutMapping(u => u.Id.Equals(id)).FirstOrDefaultAsync();
            if (user != null)
            {
                DeleteWithoutMapping(user);
                await SaveChangesAsync();
            }
        }
        public async Task<bool> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await GetWithoutMapping(u => u.Email.Equals(model.Email)).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Otp = GenerateOTP();
                user.UpdatedOn = DateTime.Now;
                UpdateWithoutMapping(user);
                await SaveChangesAsync();

                _emailHelper.SendForgotPwdEmail(model.Email, user.Otp);
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> ResetPassword(NewPasswordViewModel model)
        {
            var user = await GetWithoutMapping(u => u.Email.Equals(model.Email) && u.Otp.Equals(model.OTP)).FirstOrDefaultAsync();
            if (user != null)
            {

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                user.UpdatedOn = DateTime.Now;
                UpdateWithoutMapping(user);
                await SaveChangesAsync();

                // Send password reset email
                _emailHelper.SendConfirmationResetPassword(user.FirstName, user.Email);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ListUserModel>> GetAllUsersByAdminAsync(Guid UserId)
        {
            List<ListUserModel> users = AllUsers(null, UserId);
            // var mres = await mapper.ProjectTo<ListUserModel>(users).ToListAsync();

            return users;
        }

        private List<ListUserModel> AllUsers(Guid? CompanyId = null, Guid? UserId = null)
        {
            return (from u in databaseContext.User
                    join ur in databaseContext.UserRole on u.Id equals ur.UserId
                    join r in databaseContext.Role on ur.RoleId equals r.RoleId
                    join o in databaseContext.Company on u.CompanyId equals o.CompanyId
                    join st in databaseContext.CompanySize on o.CompanySizeId equals st.Id into gj
                    from subpet in gj.DefaultIfEmpty()
                    where (CompanyId != null && u.CompanyId == CompanyId) || (UserId != null && u.InvitedBy == UserId)
                    select new ListUserModel
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        EmailConfirmed = u.EmailConfirmed,
                        EmailConfirmedOn = u.EmailConfirmedOn,
                        PhoneNumber = u.PhoneNumber,
                        CreatedOn = u.CreatedOn,
                        UpdatedOn = u.UpdatedOn,
                        UpdatedBy = u.UpdatedBy,
                        Photo = u.Photo,
                        InvitedBy = u.InvitedBy,
                        InvitedOn = u.InvitedOn,
                        Company = o.CompanyName,
                        CompanySizeId = o.CompanySizeId ?? null,
                        CompanySize = subpet.Size ?? null,
                        RoleId = r.RoleId,
                        RoleName = r.RoleName,
                        CompanyId = o.CompanyId
                    }).OrderBy(xx => xx.CreatedOn).ToList();
        }

        public async Task<List<ListUserModel>> GetAllUsersBySuperAdminAsync(Guid CompanyId)
        {
            List<ListUserModel> users = AllUsers(CompanyId, null);

            return users;
        }

        public async Task<string> UploadPic(ProfilePicModel model)
        {
            var user = await GetWithoutMapping(u => u.Id.Equals(model.Id)).FirstOrDefaultAsync();

            if (user != null)
            {
                user.Photo = model.Photo;
                user.UpdatedOn = DateTime.Now;
                UpdateWithoutMapping(user);
                await SaveChangesAsync();

                // Send account updated notification
                _emailHelper.SendAccountUpdateEmail(user.FirstName, user.Email);
                return "200";
            }
            else
                return "404";
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await GetWithoutMapping(u => u.Id.Equals(model.Id)).FirstOrDefaultAsync();

            if (VerifyPasswordHash(model.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.NewPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UpdatedOn = DateTime.Now;
                UpdateWithoutMapping(user);
                await SaveChangesAsync();

                // Send account updated notification
                _emailHelper.SendConfirmationResetPassword(user.FirstName, user.Email);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<byte[]> GetProfilePicAsync(Guid id)
        {
            var user = await GetWithoutMapping(u => u.Id.Equals(id)).FirstOrDefaultAsync();
            if (user != null)
            {
                return user.Photo;
            }
            else
                return null;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");


            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// User accept Invitation by Company
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> AcceptInvite(UserRegisterModel user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                // throw new AppException("Password is required");
                return "404";
            //if (GetAll().Any(x => x.Email == user.Email))
            //    throw new AppException("Email \"" + user.Email + "\" is already taken");
            try
            {
                var userdetail = await GetWithoutMapping(u => u.Id.Equals(user.Id)).FirstOrDefaultAsync();

                var inviteeDetail = await GetUserByIdAsync(user.Id);


                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                userdetail.PasswordHash = passwordHash;
                userdetail.PasswordSalt = passwordSalt;
                userdetail.EmailConfirmed = true;
                userdetail.EmailConfirmedOn = DateTime.Now;
                userdetail.Otp = null;
                userdetail.UpdatedOn = DateTime.Now;
                userdetail.UpdatedBy = user.Id;
                userdetail.FirstName = user.FirstName;
                userdetail.LastName = user.LastName;
                userdetail.Email = user.Email;
                userdetail.PhoneNumber = user.PhoneNumber;


                if (userdetail.Id != null)
                    UpdateWithoutMapping(userdetail);

                await SaveChangesAsync();

                // Send otp to email to confirm account            
                _emailHelper.SendConfirmationEmail(userdetail.FirstName, userdetail.Email, inviteeDetail.Company, inviteeDetail.RoleName);
                return "200";
            }
            catch
            {
                return "400";
            }
        }
    }
}
