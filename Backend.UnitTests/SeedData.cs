using Backend.Core.Entities;
using Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.UnitTests
{
    public static class SeedData
    {

        //ini ToDoListTask 1
        public static readonly Order order1 = new Order
        {
            Id = 1,
            OrderNo = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            Status = "Order PickUp",
            IsDeleted = false,
        };

        //ini ToDoListTask 2
        public static readonly Order order2 = new Order
        {
            Id = 2,
            OrderNo = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            Status = "Process at Warehouse",
            IsDeleted = false,
        };

        //ini ToDoListTask 3
        public static readonly Order order3 = new Order
        {
            Id = 3,
            OrderNo = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            Status = "Order send to transit center",
            IsDeleted = false,
        };

        //ini ToDoListTask 4
        public static readonly Order order4 = new Order
        {
            Id = 4,
            OrderNo = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            Status = "Order receive on transit center",
            IsDeleted = false,
        };

        //ini ToDoListTask 5
        public static readonly Order order5 = new Order
        {
            Id = 5,
            OrderNo = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            Status = "Order On delivery to received",
            IsDeleted = false,
        };

        //ToDoListTaskForNew 
        public static readonly Order OrderForNew = new Order
        {
            Id = 6,
            OrderNo = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            Status = "Order Pick Up",
            IsDeleted = false,
        };

        //ToDoListTaskForDelete 
        public static readonly Order ToDoListTaskForDelete = new Order
        {
            Id = 6,
            OrderNo = Guid.NewGuid(),
            UserId = Guid.NewGuid().ToString(),
            Status = "Order Pick Up",
            IsDeleted = false,
        };
    }
}
