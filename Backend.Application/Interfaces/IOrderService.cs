using Backend.Application.Dto;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces
{
    public interface IOrderService
    {
        Task<(bool Created, string Message)> CreateOrder(OrderPostDTO post, string userId);
    }
}
