using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RestaurantsApplication.Models;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string Image { get; set; }
    
    public int RestaurantId { get; set; }
    [ValidateNever]
    public Restaurant? Restaurant { get; set; }
}