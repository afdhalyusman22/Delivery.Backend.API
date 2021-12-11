using AutoMapper;
using Backend.API.Controllers;
using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Application.Mappers;
using Backend.Application.Services;
using Backend.Core.Entities;
using Backend.Core.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.UnitTests
{
    public class orderControllerTest
    {
        private readonly IList<Order> Order;
        private readonly IList<OrderPostDTO> OrderDTO;
        private readonly Mock<IRepository> mockRepo;
        private readonly Mock<IOrderService> mockOrderService;
        private readonly IMapper mockMapper;
        private readonly OrderController controller;
        private readonly OrderService service;

        public orderControllerTest()
        {

            this.mockRepo = new Mock<IRepository>();
            this.mockOrderService = new Mock<IOrderService>();
            var mappingProfile = new MappingProfile();
            var conifg = new MapperConfiguration(mappingProfile);
            mockMapper = new Mapper(conifg);

            this.controller = new OrderController(this.mockRepo.Object, this.mockOrderService.Object, this.mockMapper);

            Order = new List<Order>
            {
                SeedData.order1,
                SeedData.order2,
                SeedData.order3
            };

            mockRepo.Setup(repo => repo.GetByIdAsync<Order>(1)).ReturnsAsync(Order.Where(i => i.Id == 1).FirstOrDefault()).Verifiable();
            mockRepo.Setup(repo => repo.DeleteAsync<Order>(1)).ReturnsAsync((true, "Data successfully deleted")).Verifiable();
            mockRepo.Setup(repo => repo.GetQueryable<Order>()).Verifiable();

            mockRepo.Setup(repo => repo.ListAsync<Order>(1000)).ReturnsAsync(Order.ToList()).Verifiable();
        }

        [Test]
        public async Task GetAllOrder_ActionExecutes_ReturnsNotFound()
        {
            var result = await controller.ListOrder();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllOrder_ActionExecutes_Returns500()
        {
            mockRepo.Setup(repo => repo.ListAsync<Order>()).Throws<Exception>().Verifiable();
            var result = await controller.ListOrder();

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllOrder_ActionExecutes_ReturnsNumbersOfToDoTaskAsync()
        {
            var result = await controller.ListOrder();
            var oor = result as ObjectResult;
            Assert.IsNotNull(oor);
            Assert.AreEqual(200, oor.StatusCode);
        }

        [Test]
        public async Task GetSpecificOrder_ActionExecutes_ReturnsCorrectValue()
        {
            var result = await controller.GetById(1);
            var oor = result as ObjectResult;

            Assert.IsNotNull(oor);
            Assert.AreEqual(200, oor.StatusCode);
        }

        [Test]
        public async Task GetSpecificOrder_ActionExecutes_ReturnsNotFound()
        {
            var result = await controller.GetById(-1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetSpecificToDo_ActionExecutes_ReturnsError()
        {
            mockRepo.Setup(repo => repo.GetByIdAsync<Order>(-1)).Throws<Exception>().Verifiable();
            var result = await controller.GetById(-1);

            var oor = result as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(500, oor.StatusCode);
        }
    }
}
