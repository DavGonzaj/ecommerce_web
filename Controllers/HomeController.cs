using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ecommerce_web.Models;

// Add the correct namespace for ApplicationDbContext if different
// using YourProjectNamespace.Data;

public class HomeController : Controller
{
    private readonly MyContext _context;

    public HomeController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }

    public IActionResult ProductDetails(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }
}