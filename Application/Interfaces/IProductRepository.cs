using Server.Application.Models;

namespace Server.Application.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product Product);
        Task<bool> IsProductNameExistingAsync(string ProductName);  
    }
}
