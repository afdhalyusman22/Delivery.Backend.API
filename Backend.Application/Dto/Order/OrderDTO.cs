using Backend.Core.Entities;
using System;
using System.Collections.Generic;

namespace Backend.Application.Dto
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public Guid OrderNo { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
        public List<OrderLogDTO> OrderLogs { get; set; }
    }

    public class OrderDetailDTO
    {
        public long OrderId { get; set; }
        public string Sender { get; set; }
        public string SenderPhoneNo { get; set; }
        public string SenderAddress { get; set; }
        public string Recipient { get; set; }
        public string RecipientPhoneNo { get; set; }
        public string RecipientAddress { get; set; }
        public decimal WeightOrder { get; set; }
        public string ItemOrder { get; set; }
    }

    public class OrderLogDTO
    {
        public long OrderId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
