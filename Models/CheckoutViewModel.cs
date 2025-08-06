using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ecommerce_web.Models
{
    public class CheckoutViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public bool TermsAccepted { get; set; }

        public List<CartItem> CartItems { get; set; } = new();
    }
}
