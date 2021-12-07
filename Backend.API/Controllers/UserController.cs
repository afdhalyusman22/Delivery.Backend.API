using Backend.API.Error;
using Backend.Application.Dto.User;
using Backend.Application.Interfaces;
using Backend.Core.Entities;
using Backend.Core.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _user;
        private readonly IRepository _repository;
        public UserController(IUsersService user, IRepository repository)
        {
            _user = user;
            _repository = repository;
        }

        [HttpPost("authenticate")]
        [Produces("application/json")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var PasswordHash = Others.GenerateHash(users.Password);

                    var (Authenticated, Result, Message) = await _user.AuthenticateAsync(users.UserName, PasswordHash);

                    if (!Authenticated && Message != "")
                        return Requests.Response(this, new ApiStatus(500), Result, Message);

                    return Requests.Response(this, new ApiStatus(200), Result, Message);
                }
                else
                {
                    return Requests.Response(this, new ApiStatus(500), ModelState, null);
                }
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("ping")]
        public IActionResult TestingUsersAsync()
        {
            try
            {

                return Requests.Response(this, new ApiStatus(200), "OK", "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }

        }
    }
}
