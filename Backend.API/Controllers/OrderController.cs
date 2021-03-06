using AutoMapper;
using Backend.API.Error;
using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Core.Entities;
using Backend.Core.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        private readonly IRepository _repository;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IRepository repository, IOrderService orderService, IMapper mapper)
        {
            _repository = repository;
            _orderService = orderService;
            _mapper = mapper;
        }
        /// <summary>
        /// Create Order For Client
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateOrder([FromBody] OrderPostDTO post)
        {
            try
            {
                string userid = GetUserIdOnly();
                if (userid == null)
                    return Requests.Response(this, new ApiStatus(401), null, "You dont have acces to create order");

                var created = await _orderService.CreateOrder(post, userid);

                return !created.Created ? Requests.Response(this, new ApiStatus(500), null, created.Message) : Requests.Response(this, new ApiStatus(200), null, created.Message);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }

        }
        /// <summary>
        /// Get Detail Order By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                OrderDTO result = new OrderDTO();
                Order items = new Order();
                string role = GetRole();
                string userId = GetUserIdOnly();
                if (role == null || userId == null)
                    return Requests.Response(this, new ApiStatus(401), null, "You dont have acces to get all order");

                if (role.ToLower() == "admin")
                    items = await _repository.GetByIdAsync<Order>(id, x => x.OrderDetails, x => x.OrderLogs);
                else
                {
                    var order = await _repository.GetQueryableWithWhereAsync<Order>(x => x.UserId == userId && x.Id == id, 0, x => x.OrderDetails, x => x.OrderLogs);
                    items = order.FirstOrDefault();
                }

                result = _mapper.Map<OrderDTO>(items);

                return Requests.Response(this, new ApiStatus(200), result, "");

            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        /// <summary>
        /// Get List Order
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> ListOrder()
        {
            try
            {
                List<OrderDTO> result = new List<OrderDTO>();
                List<Order> items = new List<Order>();
                string role = GetRole();
                string userId = GetUserIdOnly();
                if (role == null || userId == null)
                    return Requests.Response(this, new ApiStatus(401), null, "You dont have acces to get all order");

                if (role.ToLower() == "admin")
                    items = await _repository.ListAsync<Order>(1000, x => x.OrderDetails, x => x.OrderLogs);
                else
                {
                    var order = await _repository.GetQueryableWithWhereAsync<Order>(x => x.UserId == userId, 0, x => x.OrderDetails, x => x.OrderLogs);
                    items = order.ToList();
                }

                result = _mapper.Map<List<OrderDTO>>(items);
                return Requests.Response(this, new ApiStatus(200), result, "");

            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        /// <summary>
        /// UPdate Status Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDTO post)
        {
            try
            {
                OrderDTO result = new OrderDTO();
                Order items = new Order();
                string role = GetRole();
                string userId = GetUserIdOnly();
                if (role == null || userId == null)
                    return Requests.Response(this, new ApiStatus(401), null, "You dont have acces to get all order");
                
                items = await _repository.GetByIdAsync<Order>(post.Id);
                if (items == null)
                    return Requests.Response(this, new ApiStatus(404), null, "Order not exist");

                var status = await _repository.GetByIdAsync<Status>(post.Status);

                items.Status = status.Name;

                var u = await _repository.UpdateManyAsync<Order>(items);
                if (!u.Updated && (u.Message != null))
                    return Requests.Response(this, new ApiStatus(500), null, u.Message);

                OrderLog orderLog = new OrderLog();
                orderLog.OrderId = post.Id;
                orderLog.Status = status.Name;

                var a = await _repository.AddManyAsync<OrderLog>(orderLog);
                if (!a.Added && (a.Message != null))
                    return Requests.Response(this, new ApiStatus(500), null, a.Message);

                var commit = await _repository.CommitSync();
                if (!commit.Commited && (commit.Message != null))
                    return Requests.Response(this, new ApiStatus(500), null, commit.Message);

                return Requests.Response(this, new ApiStatus(200), null, "Success Update Status Order");

            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }


    }
}
