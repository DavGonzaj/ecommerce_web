using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using ecommerce_web.Models;

public class CartController : Controller
{
    private readonly MyContext _context;
    private const string CartSessionKey = "CartSession";

    public CartController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    public IActionResult AddToCart(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null || product.Stock <= 0)
            return NotFound();

        var cart = GetCart();
        var existingItem = cart.FirstOrDefault(c => c.ProductId == id);

        if (existingItem != null)
        {
            if (existingItem.Quantity < product.Stock)
                existingItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1
            });
        }

        SaveCart(cart);
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int id)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.ProductId == id);
        if (item != null)
        {
            cart.Remove(item);
            SaveCart(cart);
        }
        return RedirectToAction("Index");
    }

    private List<CartItem> GetCart()
    {
        var sessionCart = HttpContext.Session.GetString(CartSessionKey);
        return string.IsNullOrEmpty(sessionCart)
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(sessionCart);
    }

    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
    }
}
