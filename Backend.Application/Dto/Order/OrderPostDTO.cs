
using System;

namespace Backend.Application.Dto
{
    public class OrderPostDTO
    {
        public string Sender { get; set; }
        public string SenderPhoneNo { get; set; }
        public string SenderAddress { get; set; }
        public string Recipient { get; set; }
        public string RecipientPhoneNo { get; set; }
        public string RecipientAddress { get; set; }
        public decimal WeightOrder { get; set; }
        public string ItemOrder { get; set; }
    }

    public class OrderUpdateDTO
    {
        public long Id { get; set; }
        public Guid OrderNo { get; set; }
        public int Status { get; set; }
    }
}
