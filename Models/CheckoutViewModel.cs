using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "You must agree to the terms.")]
        public bool TermsAccepted { get; set; }

        public List<CartItem> CartItems { get; set; } = new();
    }
}
