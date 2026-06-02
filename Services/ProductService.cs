using ISO_ERP.Data;
using ISO_ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ISO_ERP.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
            {
                return await _context.Products
                    .AsNoTracking()
                    .Where(x => x.IsActive)
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Product product, List<int> detailIds)
            {
                _context.Products.Add(product);

                await _context.SaveChangesAsync();

                foreach (var detailId in detailIds)
                {
                    _context.ProductDetails.Add(new ProductDetail
                    {
                        ProductId = product.Id,
                        DetailId = detailId
                    });
                }

                await _context.SaveChangesAsync();
            }
        
        public async Task UpdateAsync(Product product, List<int> detailIds)
            {
                var existingProduct = await _context.Products
                    .Include(x => x.ProductDetails)
                    .FirstOrDefaultAsync(x => x.Id == product.Id);

                if (existingProduct == null)
                    return;

                existingProduct.Name = product.Name;

                existingProduct.Code = product.Code;

                existingProduct.Notes = product.Notes;

                _context.ProductDetails.RemoveRange(existingProduct.ProductDetails);

                foreach (var detailId in detailIds)
                {
                    _context.ProductDetails.Add(new ProductDetail
                    {
                        ProductId = product.Id,
                        DetailId = detailId
                    });
                }

                await _context.SaveChangesAsync();
            }

        public async Task<Product?> GetByIdAsync(int id)
            {
                return await _context.Products
                    .Include(x => x.Category)
                    .Include(x => x.ProductDetails)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
        
        public async Task SoftDeleteAsync(int id)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return;

                product.IsActive = false;

                await _context.SaveChangesAsync();
            }
        
        public async Task<List<Product>> GetAllWithInactiveAsync()
            {
                return await _context.Products
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }

    }
}