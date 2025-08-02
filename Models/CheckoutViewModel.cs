using System.ComponentModel.DataAnnotations;

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

    public List<OrderItem> CartItems { get; set; }

    [Required]
    public bool TermsAccepted { get; set; }
}
