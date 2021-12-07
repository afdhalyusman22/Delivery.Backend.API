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
        public static readonly Order ToDoListTask1 = new Order
        {
            Id = 1,
            OrderNo = Guid.NewGuid(),
            UserId = "",
            Status = "",
            IsDeleted = false,
        };

        //ini ToDoListTask 2
        public static readonly Order ToDoListTask2 = new Order
        {
            Id = 2,
            OrderNo = Guid.NewGuid(),
            UserId = "",
            Status = "",
            IsDeleted = false,
        };

        //ini ToDoListTask 3
        public static readonly Order ToDoListTask3 = new Order
        {
            Id = 3,
            OrderNo = Guid.NewGuid(),
            UserId = "",
            Status = "",
            IsDeleted = false,
        };

        //ini ToDoListTask 4
        public static readonly Order ToDoListTask4 = new Order
        {
            Id = 4,
            OrderNo = Guid.NewGuid(),
            UserId = "",
            Status = "",
            IsDeleted = false,
        };

        //ini ToDoListTask 5
        public static readonly Order ToDoListTask5 = new Order
        {
            Id = 5,
            OrderNo = Guid.NewGuid(),
            UserId = "",
            Status = "",
            IsDeleted = false,
        };

        //ToDoListTaskForNew 
        public static readonly Order ToDoListTaskForNew = new Order
        {
            Id = 6,
            OrderNo = Guid.NewGuid(),
            UserId = "",
            Status = "",
            IsDeleted = false,
        };

        //ToDoListTaskForDelete 
        public static readonly Order ToDoListTaskForDelete = new Order
        {
            Id = 6,
            OrderNo = Guid.NewGuid(),
            UserId = "",
            Status = "",
            IsDeleted = false,
        };
    }
}
