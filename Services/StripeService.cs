using Stripe;

public class StripeService
{
    public StripeService(IConfiguration config)
    {
        StripeConfiguration.ApiKey = config["Stripe:SecretKey"];
    }

    public PaymentIntent CreatePaymentIntent(long amount, string currency = "usd")
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = currency,
            PaymentMethodTypes = new List<string> { "card" },
        };
        var service = new PaymentIntentService();
        return service.Create(options);
    }
}