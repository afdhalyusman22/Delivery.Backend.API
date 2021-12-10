using AutoMapper;
using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Core.Entities;
using Backend.Core.Repositories.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Backend.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;
        private readonly IConfiguration configs;
        private readonly IMapper mapper;

        public OrderService(IRepository _repository, IConfiguration _configs, IMapper _mapper)
        {
            this.repository = _repository;
            this.configs = _configs;
            this.mapper = _mapper;
        }

        public async Task<(bool Created, string Message)> CreateOrder(OrderPostDTO post, string userId)
        {
            try
            {
                OrderDetail orderDetail = mapper.Map<OrderDetail>(post);
                OrderLog orderLog = new OrderLog
                {
                    Status = Status.PickUp
                };
                Guid orderNumber = Guid.NewGuid();
                Order order = new Order();
                order.UserId = userId;
                order.OrderNo = orderNumber;
                order.Status = Status.PickUp;
                order.OrderDetails.Add(orderDetail);
                order.OrderLogs.Add(orderLog);

                var c = await repository.AddAsync(order);

                if (!c.Added && (c.Message != null))
                    return (false, c.Message);

                return (true, "Success Created Order. Order No : " + orderNumber);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
