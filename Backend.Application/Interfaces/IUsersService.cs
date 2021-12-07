using System.Threading.Tasks;

namespace Backend.Application.Interfaces
{
    public interface IUsersService
    {
        Task<(bool Authenticated, object Result, string Message)> AuthenticateAsync(string username, string password);
    }
}
