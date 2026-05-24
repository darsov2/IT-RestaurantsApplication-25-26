using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantsApplication.Models;
using RestaurantsApplication.Models.ViewModels;

namespace RestaurantsApplication.Controllers;

[Authorize(Roles = "Admin")]
public class AccountController : Controller
{
    private readonly UserManager<RestaurantApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public AccountController(UserManager<RestaurantApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult AddUserToRole()
    {
        var users = _userManager.Users.ToList();
        var role = _roleManager.Roles.ToList();

        ViewBag.Users = new SelectList(users, "Id", "Email");
        ViewBag.Roles = new SelectList(role, "Name", "Name");
        
        return View();
    }
    
    [HttpPost]
    // public IActionResult AddUserToRole(string userId, string roleName)
    public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            return NotFound();
        }

        await _userManager.AddToRoleAsync(user, model.RoleName);
        
        return RedirectToAction(nameof(AddUserToRole));
    }
}