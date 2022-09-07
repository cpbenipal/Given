using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Given.Repositories.Generic;
using AutoMapper;
using Given.Models;
using System.Threading.Tasks;
using Given.DataContext.Entities;
using Given.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Given.DataContext.Identity;

namespace Given.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userService;
        private IMapper _mapper;
        public UsersController(
            IUserRepository userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                var IsEmailConfirmed = await _userService.IsEmailConfirmed(model.Email);
                if (IsEmailConfirmed.Equals(string.Empty))
                {

                    var user = await _userService.Authenticate(model.Email, model.Password);

                    if (user == null)
                    {
                        apiResult.ReturnData = "";
                        apiResult.ReturnCode = StatusCodes.Status404NotFound;
                        apiResult.ReturnMessage = "Authentication failed. The email or password you entered is incorrect.";
                        apiResult.ReturnStatus = false;
                    }
                    else
                    {
                        apiResult.ReturnData = user;
                        apiResult.ReturnCode = StatusCodes.Status200OK;
                        apiResult.ReturnMessage = "User Login Successful!. Please continue..";
                        apiResult.ReturnStatus = true;
                    }
                    // return basic user info and authentication token
                    //return Ok(user);
                }
                else if (IsEmailConfirmed.Equals("401"))
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status401Unauthorized;
                    apiResult.ReturnMessage = "Verification Pending. Confirm your email to continue. You have received an email with a link and OTP. Please follow the instructions in the email";
                    apiResult.ReturnStatus = false;
                }
                else //if (IsEmailConfirmed.Equals("401"))
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status401Unauthorized;
                    apiResult.ReturnMessage = "That email does not exist!. Enter a different account or Register!";
                    apiResult.ReturnStatus = false;
                }
            }
            catch (AppException x)
            {
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = x.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }

        [HttpGet("Logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Cookies");
            Response.Cookies.Delete("BearerToken");
            return Ok();
        }
        //[AllowAnonymous]
        //[HttpPost("SeedData")]
        //public ActionResult SeedData()
        //{
        //    _userService.SeedData();
        //    return NoContent();
        //}
        [AllowAnonymous]
        [HttpPost("PostCompany")]
        public async Task<IActionResult> PostCompany([FromBody]RegisterModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                // create user
                var response = await _userService.Create(model, model.Password);
                if (response == "404")
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status404NotFound;
                    apiResult.ReturnMessage = "Password is required";
                    apiResult.ReturnStatus = false;
                }
                else if (response == "412")
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = Convert.ToInt32(response);
                    apiResult.ReturnMessage = "We're sorry. Email or company name is already taken";
                    apiResult.ReturnStatus = false;
                }
                else if (response == "200")
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "Thank you for signing up. Please verify your email account with OTP sent to your email.";
                    apiResult.ReturnStatus = true;
                }
            }
            catch (AppException x)
            {
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = x.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }
        [AllowAnonymous]
        [HttpPost("PostUser")]
        public async Task<IActionResult> PostUser([FromBody]UserRegisterModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                var verify = await _userService.VerifyUser(model.Email, model.Otp, model.InvitedBy);
                if (verify)
                {

                    // create user 
                    var response = await _userService.AcceptInvite(model, model.Password);
                    if (response == "200")
                    {
                        // return StatusCode(StatusCodes.Status201Created);
                        apiResult.ReturnData = "";
                        apiResult.ReturnCode = StatusCodes.Status201Created;
                        apiResult.ReturnMessage = "Awesome ! Your account has been confirmed. Please login.";
                        apiResult.ReturnStatus = true;
                    }
                    else if (response == "400")
                    {
                        // return error message if there was an exception
                        apiResult.ReturnData = "";
                        apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                        apiResult.ReturnMessage = "Internal application error!.";
                        apiResult.ReturnStatus = false;
                    }
                    else
                    {
                        // return error message if there was an exception
                        apiResult.ReturnData = "";
                        apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                        apiResult.ReturnMessage = "Password is required";
                        apiResult.ReturnStatus = false;
                    }
                }
                else
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status401Unauthorized;
                    apiResult.ReturnMessage = "Verification failed. The email or OTP you entered is incorrect. Please check email and try again";
                    apiResult.ReturnStatus = false;
                    // return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
            catch (AppException x)
            {
                // return error message if there was an exception
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = x.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }
        [AllowAnonymous]
        [HttpPost("ConfirmAccount")]

        public async Task<IActionResult> ConfirmCompany([FromBody]ConfirmEmailModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                // create user
                bool response = await _userService.ConfirmCompany(model);
                if (response)
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "Awesome ! Company account has been confirmed. Please login.";
                    apiResult.ReturnStatus = true;
                }
                else
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status401Unauthorized;
                    apiResult.ReturnMessage = "Verification failed. The email or OTP you entered is incorrect. Please check email and try again";
                    apiResult.ReturnStatus = false;
                }
            }
            catch (AppException x)
            {
                // return error message if there was an exception
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = x.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }
        //[Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost("InviteUser")]
        public async Task<IActionResult> InviteUser([FromBody]InviteViewModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                // create user
                var response = await _userService.InviteUser(model);
                if (response == "200")
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "Invitation sent.";
                    apiResult.ReturnStatus = true;
                }
                else
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status404NotFound;
                    apiResult.ReturnMessage = "Email is already taken.";
                    apiResult.ReturnStatus = false;
                }
                // return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (AppException x)
            {
                // return error message if there was an exception
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = x.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }
        [AllowAnonymous] 
        [HttpPut("PutUser")]
        public async Task<IActionResult> PutUser([FromBody]UpdateModel model)
        {
            var apiResult = new ApiResult();
           // var user = _mapper.Map<User>(model);
            try
            {
                // update user 
                var response = await _userService.Update(model, null);
                if (response == "200")
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "Profile updated.";
                    apiResult.ReturnStatus = true;
                }
                else if (response == "100")
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "Email is already taken";
                    apiResult.ReturnStatus = false;
                }
                else if (response == "404")
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "User not found!";
                    apiResult.ReturnStatus = false;
                }
            }
            catch (AppException x)
            {
                // return error message if there was an exception
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = x.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }

        [AllowAnonymous]
        [HttpPut("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                var response = await _userService.ForgotPassword(model);
                if (response)
                {
                    //"You'hv received an OTP on your email.Please check your email."
                    //  return StatusCode(StatusCodes.Status200OK);
                    apiResult.ReturnData = "";
                    apiResult.ReturnStatus = true;
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "You'hv received an OTP on your email.Please check your email.";
                }
                else
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnStatus = false;
                    apiResult.ReturnCode = StatusCodes.Status404NotFound;
                    apiResult.ReturnMessage = "Email does not exist in the system!";
                    //"Email does not exist in the system!" 
                }
            }
            catch (AppException x)
            {
                // return error message if there was an exception
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = x.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                var response = await _userService.ChangePassword(model);
                if (response)
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "Password Changed Successfully.";
                    apiResult.ReturnStatus = true;
                    // return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "Current password mismatch.";
                    apiResult.ReturnStatus = false;
                }
            }
            catch (AppException x)
            {
                // return error message if there was an exception
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = x.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }

        [AllowAnonymous]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]NewPasswordViewModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                bool response = await _userService.ResetPassword(model);
                if (response)
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "You have successfully reset your password ! Please login with new password.";
                    apiResult.ReturnStatus = true;
                    // return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "The email or OTP you entered is incorrect.";
                    apiResult.ReturnStatus = false;
                }
            }
            catch (AppException ex)
            {
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = ex.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("GetAllUsersBySuperAdmin/{CompanyId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListUserModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]   
        public async Task<IActionResult> GetAllBySuperAdmin(Guid CompanyId)
        {
            try
            {
                var model = await _userService.GetAllUsersBySuperAdminAsync(CompanyId);
                return Ok(model);
            }
            catch (AppException x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAllUsersByAdmin/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListUserModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]  
        public async Task<IActionResult> GetAllByAdmin(Guid UserId)
        {
            try
            {
                var model = await _userService.GetAllUsersByAdminAsync(UserId);
                return Ok(model);
            }
            catch (AppException x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListUserModel>))]  
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var model = await _userService.GetUserByIdAsync(id);
            return Ok(model);
        }

        [HttpGet("GetProfilePic/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfilePic(Guid id)
        {
            var model = await _userService.GetProfilePicAsync(id);
            return Ok(model);
        }

        [HttpPost("UploadPic")]
        public async Task<IActionResult> UploadPic([FromBody]ProfilePicModel model)
        {
            var apiResult = new ApiResult();
            try
            {
                var response = await _userService.UploadPic(model);
                if (response == "200")
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status200OK;
                    apiResult.ReturnMessage = "Profile photo has been successfully uploaded. Note: Please refresh once if did not see profile photo.";
                    apiResult.ReturnStatus = true;
                    // return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    apiResult.ReturnData = "";
                    apiResult.ReturnCode = StatusCodes.Status404NotFound;
                    apiResult.ReturnMessage = "User not found.";
                    apiResult.ReturnStatus = false;
                }
            }
            catch (AppException ex)
            {
                apiResult.ReturnData = "";
                apiResult.ReturnCode = StatusCodes.Status400BadRequest;
                apiResult.ReturnMessage = ex.InnerException.Message;
                apiResult.ReturnStatus = false;
            }
            return Ok(apiResult);
        }
    }
}