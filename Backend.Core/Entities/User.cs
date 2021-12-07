using Backend.Core.Entities.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Backend.Core.Entities
{
    public partial class User : BaseEntityIdString
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
