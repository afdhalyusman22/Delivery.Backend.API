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
        [HttpPost("create")]
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
        /// Get List Order User Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet("list/admin")]
        public async Task<IActionResult> ListOrderAdmin()
        {
            try
            {
                string username = GetUsername();
                if (username == null)
                    return Requests.Response(this, new ApiStatus(401), null, "You dont have acces to create order");

                //for performance let 1000 if want see all set 0
                var items = await _repository.ListAsync<Order>(1000);

                var result = _mapper.Map<List<OrdetListDTO>>(items);

                return Requests.Response(this, new ApiStatus(200), result, "");

            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}
