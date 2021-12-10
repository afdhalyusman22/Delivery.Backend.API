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
        [HttpGet("GetUsername")]
        public string GetUsername()
        {
            string userName = null;
            try
            {
                if (User.Claims.Any())
                    userName = User.Claims.FirstOrDefault(x => x.Type == "initialName").Value.Split("|")[1];

            }
            catch (Exception)
            {

            }
            return userName;
        }
    }
}
