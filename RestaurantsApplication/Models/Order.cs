using System.ComponentModel.DataAnnotations;

namespace RestaurantsApplication.Models;

public class Order
{
    public int Id { get; set; }
    public string FullName { get; set; }
    [RegularExpression("07(1|2|3|4|5|6|7|8)\\d{6}")]
    public string PhoneNumber { get; set; }
    public DateTime DateCreated { get; set; }
    
    // public List<MenuItem> MenuItems { get; set; }
}