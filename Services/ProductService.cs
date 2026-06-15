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
                    .Include(x => x.Category)
                    .Include(x => x.ProductDetails)
                        .ThenInclude(x => x.SubItems)
                    .OrderBy(x => x.Category != null ? x.Category.Name : "")
                    .ThenBy(x => x.Name)
                    .ToListAsync();
            }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(
                Product product,
                List<ProductDetailCreateModel> productDetails)
            {
                _context.Products.Add(product);

                await _context.SaveChangesAsync();

                foreach (var detail in productDetails)
                {
                    var productDetail = new ProductDetail
                        {
                            ProductId = product.Id,
                            DetailId = detail.DetailId,
                            DisplayOrder = detail.DisplayOrder
                        };

                        _context.ProductDetails.Add(productDetail);

                        await _context.SaveChangesAsync();

                        foreach (var subItem in detail.SubItems)
                        {
                            _context.ProductDetailSubItems.Add(new ProductDetailSubItem
                            {
                                ProductDetailId = productDetail.Id,
                                Name = subItem.Name,
                                DisplayOrder = subItem.DisplayOrder
                            });
                        }
                }

                await _context.SaveChangesAsync();
            }
        
        public async Task AddAsync(
                Product product,
                List<ProductDetail> productDetails)
            {
                _context.Products.Add(product);

                await _context.SaveChangesAsync();
            }
        public async Task UpdateAsync(Product product, List<int> detailIds)
            {
                var existingProduct = await _context.Products
                    .Include(x => x.ProductDetails)
                            .ThenInclude(x => x.SubItems)
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

        public async Task UpdateAsync(
                Product product,
                List<ProductDetailCreateModel> productDetails)
            {
                var existingProduct = await _context.Products
                    .Include(x => x.ProductDetails)
                        .ThenInclude(x => x.SubItems)
                    .FirstOrDefaultAsync(x => x.Id == product.Id);

                if (existingProduct == null)
                    return;

                existingProduct.Name = product.Name;
                existingProduct.Code = product.Code;
                existingProduct.Notes = product.Notes;

                // _context.ProductDetails.RemoveRange(existingProduct.ProductDetails);

                // await _context.SaveChangesAsync();

                var removedDetails = existingProduct.ProductDetails
                    .Where(x => !productDetails.Any(d => d.DetailId == x.DetailId))
                    .ToList();

                _context.ProductDetails.RemoveRange(removedDetails);

                foreach (var detail in productDetails)
                    {
                        var existingDetail = existingProduct.ProductDetails
                            .FirstOrDefault(x => x.DetailId == detail.DetailId);

                        if (existingDetail != null)
                        {
                            existingDetail.DisplayOrder = detail.DisplayOrder;
                            continue;
                        }

                        var productDetail = new ProductDetail
                        {
                            ProductId = product.Id,
                            DetailId = detail.DetailId,
                            DisplayOrder = detail.DisplayOrder
                        };

                        _context.ProductDetails.Add(productDetail);

                        await _context.SaveChangesAsync();

                        foreach (var subItem in detail.SubItems)
                        {
                            _context.ProductDetailSubItems.Add(new ProductDetailSubItem
                            {
                                ProductDetailId = productDetail.Id,
                                Name = subItem.Name,
                                DisplayOrder = subItem.DisplayOrder
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
            }

        public async Task<Product?> GetByIdAsync(int id)
            {
                return await _context.Products
                    .Include(x => x.Category)
                    .Include(x => x.ProductDetails)
                            .ThenInclude(x => x.SubItems)
                    .Include(x => x.ProductDetails)
                            .ThenInclude(x => x.Detail)
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
        
        public async Task<List<Product>> GetActiveAsync()
            {
                return await _context.Products
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .Include(x => x.ProductDetails)
                        .ThenInclude(x => x.SubItems)
                    .Where(x => x.IsActive)
                    .OrderBy(x => x.Category != null ? x.Category.Name : "")
                    .ThenBy(x => x.Name)
                    .ToListAsync();
            }
        
        public async Task AddSubItemAsync(int productDetailId, string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                    return;

                var maxOrder = _context.ProductDetailSubItems
                    .Where(x => x.ProductDetailId == productDetailId)
                    .Select(x => (int?)x.DisplayOrder)
                    .Max() ?? 0;

                _context.ProductDetailSubItems.Add(new ProductDetailSubItem
                    {
                        ProductDetailId = productDetailId,
                        Name = name.Trim(),
                        DisplayOrder = maxOrder + 1
                    });

                await _context.SaveChangesAsync();
            }

        public async Task DeleteSubItemAsync(int id)
            {
                var subItem = await _context.ProductDetailSubItems
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (subItem == null)
                    return;

                _context.ProductDetailSubItems.Remove(subItem);

                await _context.SaveChangesAsync();
            }

    }
}