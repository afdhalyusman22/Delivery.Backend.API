using Backend.Core.Entities.Base;
using System;

#nullable disable

namespace Backend.Core.Entities
{
    public partial class OrderLog : BaseEntity
    {
        public long OrderId { get; set; }
        public string Status { get; set; }
    }
}
