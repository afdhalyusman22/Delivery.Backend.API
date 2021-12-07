using Backend.Core.Entities.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Backend.Core.Entities
{
    public partial class OrderDetail : BaseEntity
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

        public virtual Order Order { get; set; }
    }
}
