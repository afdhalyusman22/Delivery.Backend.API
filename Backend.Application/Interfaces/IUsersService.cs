using Backend.Core.Entities;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces
{
    public interface IUsersService
    {
        Task<(bool Authenticated, object Result, string Message)> AuthenticateAsync(User user);
    }
}
