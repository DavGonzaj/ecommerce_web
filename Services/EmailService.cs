using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using ecommerce_web.Models; // For Order model

namespace ecommerce_web.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        // ✅ Add the method here:
        public async Task SendOrderConfirmationAsync(string toEmail, string fullName, Order order)
        {
            var apiKey = _config["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@yourecommerce.com", "Your Store");
            var to = new EmailAddress(toEmail, fullName);
            var subject = $"Order Confirmation - #{order.Id}";
            var plainTextContent = $"Hi {fullName},\n\nThank you for your order!";
            var htmlContent = $"<strong>Thank you {fullName}!</strong><br>Your order #{order.Id} has been confirmed.";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
