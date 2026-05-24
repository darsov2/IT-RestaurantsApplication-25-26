namespace RestaurantsApplication.Models.Dto;

public class RestaurantDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }
    public double Rating { get; set; }
    public int Id { get; set; }
}

//     
// [MaxLength(200)]
// public string Address { get; set; }
//     
// [StringLength(200, MinimumLength = 100)]
// public string City { get; set; }
// public string Country { get; set; }
// [Range(0, 10)]
// public double Rating { get; set; }
// public string Test { get; set; }