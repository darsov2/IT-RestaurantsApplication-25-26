using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RestaurantsApplication.Models;

public class Order
{
    public int Id { get; set; }
    public string FullName { get; set; }
    [RegularExpression("07(1|2|3|4|5|6|7|8)\\d{6}")]
    public string PhoneNumber { get; set; }
    public DateTime DateCreated { get; set; }
    
    [ValidateNever]
    public Restaurant Restaurant { get; set; }
    public int RestaurantId { get; set; }
    
    public ICollection<MenuItemInOrder> MenuItems { get; set; } = new List<MenuItemInOrder>();
}