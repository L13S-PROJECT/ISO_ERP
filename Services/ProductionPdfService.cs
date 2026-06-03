using ISO_ERP.Models;
using ISO_ERP.PDF;
using ISO_ERP.Data;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace ISO_ERP.Services;

public class ProductionPdfService
{
    public async Task<byte[]> GenerateProductionPdf(List<Production> productions)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var products = await context.Products
            .Include(x => x.ProductDetails)
                .ThenInclude(x => x.Detail)
            .ToListAsync();

        var document = Document.Create(container =>
        {
            foreach (var production in productions)
            {
                var product = products
                    .FirstOrDefault(x => x.Id == production.ProductId);

                var protocolDocument = new ProductionProtocolDocument(
                    production,
                    product);

                protocolDocument.Compose(container);
            }
        });

        return document.GeneratePdf();
    }

    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public ProductionPdfService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
}