using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantsApplication.Data;
using RestaurantsApplication.Models;
using RestaurantsApplication.Models.ViewModels;

namespace RestaurantsApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Restaurant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult CreateForRestaurant(int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var model = new CreateOrderForRestaurantViewModel()
            {
                Restaurant = restaurant,
                Order = new Order()
                {
                    RestaurantId = id,
                }
            };
            return View(model);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,PhoneNumber,DateCreated,RestaurantId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.DateCreated = DateTime.Now;
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = order.Id });
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Name", order.RestaurantId);
            return View(order);
        }

        public IActionResult AddItem(int id)
        {
            var order = _context.Orders
                .Include(o => o.Restaurant.MenuItems)
                .Include(o => o.MenuItems)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }

        public IActionResult DecrementQuantity(int orderId, int menuItemId)
        {
            var existingMenuItem = _context.MenuItemInOrders.FirstOrDefault(m => m.OrderId == orderId && m.MenuItemId == menuItemId);

            if (existingMenuItem != null)
            {
                if (existingMenuItem.Quantity == 1)
                {
                    _context.Remove(existingMenuItem);
                    _context.SaveChanges();
                }
                else
                {
                    existingMenuItem.Quantity--;
                    _context.SaveChanges();
                }
            }
            
            return RedirectToAction("AddItem", new { id = orderId });
        }

        [HttpPost]
        public IActionResult IncrementQuantity(int orderId, int menuItemId)
        {
            var exisitngMenuItem = _context.MenuItemInOrders.FirstOrDefault(x => 
                x.OrderId == orderId 
                && x.MenuItemId == menuItemId);

            if (exisitngMenuItem != null)
            {
                exisitngMenuItem.Quantity++;
                _context.SaveChanges();
            }
            else if (exisitngMenuItem == null)
            {
                var menuItemInOrder = new MenuItemInOrder()
                {
                    OrderId = orderId,
                    MenuItemId = menuItemId,
                    Quantity = 1
                };
                
                _context.MenuItemInOrders.Add(menuItemInOrder);
                _context.SaveChanges();
            }
            
            return RedirectToAction("AddItem", new { id  = orderId });
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Name", order.RestaurantId);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,PhoneNumber,DateCreated,RestaurantId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Name", order.RestaurantId);
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
