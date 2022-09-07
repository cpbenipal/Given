using Microsoft.AspNetCore.Mvc;
using Given.Repositories.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Given.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private IHomeRepository _HomeService;  
        public HomeController(
            IHomeRepository HomeService )
        {
            _HomeService = HomeService;  
        }         

        [HttpGet("GetKPIs")]     
        public async Task<IActionResult> GetKPIs()
        {
            var UserId = new Guid(HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name).Value);
            var model = await _HomeService.GetKPIs(UserId);
            return Ok(model);
        }
            
    }
}