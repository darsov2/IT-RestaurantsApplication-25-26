namespace RestaurantsApplication.Models.Dto;

public class TabulatorPagingDto
{
    public int LastPage { get; set; }
    public List<RestaurantDto> Data { get; set; }
}

// {
//     "last_page":15, //the total number of available pages (this value must be greater than 0)
//     "data":[ // an array of row data objects
//         {id:1, name:"bob", age:"23"}, //example row data object
//     ]
// }