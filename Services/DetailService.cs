using ISO_ERP.Data;
using ISO_ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ISO_ERP.Services
{
    public class DetailService
    {
        private readonly AppDbContext _context;

        public DetailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Detail>> GetAllAsync()
        {
            return await _context.Details
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}