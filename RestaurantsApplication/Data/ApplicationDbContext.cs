using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantsApplication.Models;

namespace RestaurantsApplication.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<RestaurantApplicationUser>(options)
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<MenuItemInOrder> MenuItemInOrders { get; set; }
    public DbSet<RestaurantApplicationUser> RestaurantApplicationUsers { get; set; }
}