using Microsoft.EntityFrameworkCore;
using Server.Application.Interfaces;
using Server.Application.Models;
using Server.Infrastructure.Context;

namespace Server.Application.Repositories
{
    public class ProductRespository : IProductRepository
    {
        private readonly ECommerceDBContext _context;
        public ProductRespository(ECommerceDBContext context)
        {
            _context = context;
        }
        public async Task CreateProductAsync(Product Product)
        {
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsProductNameExistingAsync(string ProductName)
        {
            bool IsExisting = await _context.Products.AnyAsync(eachProduct => eachProduct.ProductName == ProductName);
            return IsExisting;
        }
    }
}
