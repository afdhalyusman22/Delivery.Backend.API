using Backend.Core.Entities.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Backend.Core.Entities
{
    public partial class Status : BaseEntity
    {
        public string Name { get; set; }
    }
}
