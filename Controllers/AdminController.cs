using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecommerce_web.Models;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly MyContext _context;

    public AdminController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Orders()
    {
        var orders = _context.Orders != null
            ? _context.Orders.Include(o => o.Items).ToList()
            : new List<Order>();
        return View(orders);
    }

    public IActionResult Products()
    {
        var products = _context.Products != null
            ? _context.Products.ToList()
            : new List<Product>();
        return View(products);
    }

    public IActionResult Customers()
    {
        var customers = _context.Orders != null
            ? _context.Orders.Select(o => o.Email).Distinct().ToList()
            : new List<string>();
        return View(customers);
    }
}
