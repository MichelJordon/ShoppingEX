using Domain.Entities;
namespace Domain.Dtos;
public class AddProductDto
{
    public int ProductId { get; set; }
    public Tech Tech {get; set;}
    public string ProductName {get; set;}
    public decimal Price {get; set;}
}