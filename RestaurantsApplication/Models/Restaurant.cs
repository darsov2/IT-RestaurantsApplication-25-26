using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantsApplication.Models;

public class Restaurant
{
    public int Id { get; set; }
    [Required]
    [DisplayName("Restaurant Name")]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string Address { get; set; }
    
    [StringLength(200, MinimumLength = 100)]
    public string City { get; set; }
    public string Country { get; set; }
    [Range(0, 10)]
    public double Rating { get; set; }
    
    public ICollection<MenuItem>  MenuItems { get; set; }
}