using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
        [ExcludeFromCodeCoverage]
        [HttpGet("GetUserId")]
        public string GetUserIdOnly()
        {
            string CurrentUserId = null;
            try
            {
                if (User.Claims.Any())
                    CurrentUserId = User.Claims.FirstOrDefault(x => x.Type == "userId").Value.Split("|")[1];

            }
            catch (Exception)
            {

            }
            return CurrentUserId;
        }

        [ExcludeFromCodeCoverage]
        [HttpGet("GetRole")]
        public string GetRole()
        {
            string role = null;
            try
            {
                if (User.Claims.Any())
                    role = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            }
            catch (Exception)
            {

            }
            return role;
        }
    }
}
