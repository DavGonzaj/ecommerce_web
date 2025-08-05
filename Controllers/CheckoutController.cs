
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecommerce_web.Models;
public class CheckoutController : Controller
{
    private readonly MyContext _context;
    private readonly EmailService _emailService;
    private const string CartSessionKey = "CartSession";

    public CheckoutController(MyContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cart = GetCart();
        var model = new CheckoutViewModel { CartItems = cart };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitOrder(CheckoutViewModel model)
    {
        var cart = GetCart();
        if (!ModelState.IsValid || !cart.Any())
        {
            model.CartItems = cart;
            return View("Index", model);
        }

        foreach (var item in cart)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null || product.Stock < item.Quantity)
            {
                ModelState.AddModelError("", $"Insufficient stock for {item.Name}. Only {product?.Stock ?? 0} left.");
                model.CartItems = cart;
                return View("Index", model);
            }
        }

        var order = new Order
        {
            FullName = model.FullName,
            Email = model.Email,
            Phone = model.Phone,
            Address = model.Address,
            OrderDate = DateTime.Now,
            TermsAccepted = model.TermsAccepted,
            Items = cart.Select(item => new OrderItem
            {
                ProductName = item.Name,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };

        foreach (var item in cart)
        {
            var product = _context.Products.Find(item.ProductId);
            if (product != null)
            {
                product.Stock -= item.Quantity;
            }
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _emailService.SendOrderConfirmationAsync(model.Email, model.FullName, order);

        HttpContext.Session.Remove(CartSessionKey);
        TempData["OrderId"] = order.Id;
        return RedirectToAction("Confirmation");
    }

    public IActionResult Confirmation()
    {
        int? orderId = TempData["OrderId"] as int?;
        if (orderId == null) return RedirectToAction("Index", "Home");

        var order = _context.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == orderId);
        return View(order);
    }

    private List<CartItem> GetCart()
    {
        var sessionCart = HttpContext.Session.GetString(CartSessionKey);
        return string.IsNullOrEmpty(sessionCart)
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(sessionCart);
    }
}
