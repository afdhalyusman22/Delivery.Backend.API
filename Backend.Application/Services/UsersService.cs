using Backend.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Backend.Application.Dto;
using Backend.Application.Interfaces;

namespace Backend.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository repository;
        private readonly IConfiguration configs;

        public UsersService(IRepository _repository, IConfiguration _configs)
        {
            this.repository = _repository;
            this.configs = _configs;
        }

        public async Task<(bool Authenticated, object Result, string Message)> AuthenticateAsync(User user)
        {
            try
            {               

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configs.GetSection("AppSettings").GetSection("Secret").Value);
                int TokenExpired = Convert.ToInt32(configs.GetSection("AppSettings").GetSection("TokenExpired").Value);
                string Issuer = configs.GetSection("AppSettings").GetSection("Issuer").Value;
                string Audience = configs.GetSection("AppSettings").GetSection("Audience").Value;
                string hash = configs.GetSection("AppSettings").GetSection("AES-256-CBC").Value;

                //generate refresh token and base64 them
                Guid refToken = Guid.NewGuid();
                byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(refToken.ToString());
                string base64refToken = Convert.ToBase64String(encodedBytes);


                var claims = new List<Claim> {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Fullname),
                            new Claim("initialName", user.UserName),
                            new Claim("userId", user.Fullname + "|" + user.Id),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            };



                TokenExpired = TokenExpired <= 0 ? TokenExpired = 60 : TokenExpired;

                user.LastLoginDate = DateTime.Now;
                var (Updated, Message) = await repository.UpdateAsyncIdString(user);

                if (!Updated && Message != null)
                {
                    return (false, null, Message);
                }

                var token = new JwtSecurityToken(issuer: Issuer,
                                audience: Audience,
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(TokenExpired),
                                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                            );

                var r = new TokenDTO();
                r.Token = tokenHandler.WriteToken(token);
                return (true, r, "OK");

            }
            catch (Exception ex)
            {
                return (false, null, "Trouble happened! \n" + ex.Message);
            }
        }


    }
}
