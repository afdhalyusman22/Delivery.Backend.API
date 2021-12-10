
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
    public class Status
    {
        public const string PickUp = "Order picked up";
        public const string Process_Gateway = "Processed at warehouse";
        public const string Process_Send_Transit = "Order send to transit center";
        public const string Received_Transit = "Order receive on transit center";
        public const string Process_Delivery_To_Receiver = "Order On delivery to received";
        public const string OutDelivery = "Out for delivery";
        public const string Delivery = "Order has been received";

    }

    public class OrderUpdateDTO
    {
        public Guid OrderNo { get; set; }
        public string Status { get; set; }
    }
}
