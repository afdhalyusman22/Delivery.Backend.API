using Backend.Core.Entities;
using System;
using System.Collections.Generic;

namespace Backend.Application.Dto
{
    public class OrdetListDTO
    {
        public long Id { get; set; }
        public Guid OrderNo { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderLog> OrderLogs { get; set; }
    }
}
