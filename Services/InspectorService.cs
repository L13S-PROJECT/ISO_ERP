using ISO_ERP.Data;
using ISO_ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ISO_ERP.Services
{
    public class InspectorService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public InspectorService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Inspector>> GetAllAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            return await context.Inspectors
                .Where(x => x.IsActive)
                .OrderBy(x => x.FullName)
                .ToListAsync();
        }
    }
}