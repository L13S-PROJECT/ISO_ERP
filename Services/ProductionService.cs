using ISO_ERP.Data;
using ISO_ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ISO_ERP.Services
{
    public class ProductionService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public ProductionService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Production>> GetAllAsync()
                {
                    using var context = await _contextFactory.CreateDbContextAsync();

                    return await context.Productions
                        .OrderBy(x => x.StartDate)
                        .ThenBy(x => x.BatchNo)
                        .ThenBy(x => x.Id)
                        .ToListAsync();
                }

        public async Task AddAsync(Production production)
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                context.Productions.Add(production);

                await context.SaveChangesAsync();
            }
        
        public async Task UpdateAsync(Production production)
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                var existing = await context.Productions
                    .FirstOrDefaultAsync(x => x.Id == production.Id);

                if (existing == null)
                    return;

                existing.CheckedBy = production.CheckedBy;
                existing.FinishedDate = production.FinishedDate;
                existing.Notes = production.Notes;

                existing.IsCompleted = production.FinishedDate != null;

                await context.SaveChangesAsync();
            }

        public async Task DeleteAsync(int id)
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                var production = await context.Productions
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (production == null)
                    return;

                context.Productions.Remove(production);

                await context.SaveChangesAsync();
            }
        
    }

    
}