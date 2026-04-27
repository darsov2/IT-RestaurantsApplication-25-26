namespace RestaurantsApplication.Models;

public class Order
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateCreated { get; set; }
    
    // public List<MenuItem> MenuItems { get; set; }
}