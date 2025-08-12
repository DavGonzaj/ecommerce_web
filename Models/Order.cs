using System;
using System.Collections.Generic;

namespace ecommerce_web.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public bool TermsAccepted { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
