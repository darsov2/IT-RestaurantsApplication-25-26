using Microsoft.AspNetCore.Mvc;
using RestaurantsApplication.Data;

namespace RestaurantsApplication.Controllers;

public class RestaurantController : Controller
{
    private readonly ApplicationDbContext _context;

    public RestaurantController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var restuarants = _context.Restaurants.ToList();
        return View(restuarants);
    }
}