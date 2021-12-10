using Backend.API.Error;
using Backend.Application.Dto;
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
    public class UserController : BaseApiController
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
                    var user = await _repository.GetQueryableWithWhereAsync<User>(x => (x.UserName == users.UserName || x.Email == users.UserName));

                    if (!user.Any())
                    {
                        return Requests.Response(this, new ApiStatus(401), null, "Invalid username");
                    }

                    var (Authenticated, Result, Message) = await _user.AuthenticateAsync(user.FirstOrDefault());

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
