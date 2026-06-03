using ISO_ERP.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ISO_ERP.PDF.Components;

namespace ISO_ERP.PDF;

public class ProductionProtocolDocument : IDocument
{
    private readonly Production _production;
    private readonly Product? _product;

    public ProductionProtocolDocument(
        Production production,
        Product? product)
        {
            _production = production;
            _product = product;
        }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.MarginHorizontal(40);
                page.MarginVertical(25);

                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                        .PaddingBottom(10)
                        .Text("SIA \"FMM\" | Veidlapa V-1 | Versija 1.0 | 29.08.2025")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken1);

                page.Content()
                    .Element(container =>
                    {
                        ProductionProtocolPage1.Compose(
                            container,
                            _production,
                            _product);
                    });

                page.Footer()
                    .PaddingTop(10)
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(x =>
                            x.FontSize(9)
                            .FontColor(Colors.Grey.Darken1));

                        text.Span("Lapa ");

                        text.CurrentPageNumber();

                        text.Span(" no ");

                        text.TotalPages();
                    });
            });

            // PAGE 2

            int rowsPerPage = _production.Quantity <= 43 ? 43 : 40;

                for (int start = 1; start <= _production.Quantity; start += rowsPerPage)
                {
                    int end = Math.Min(start + rowsPerPage - 1, _production.Quantity);

                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);

                        page.MarginHorizontal(40);
                        page.MarginVertical(25);

                        page.DefaultTextStyle(x => x.FontSize(12));

                        page.Header()
                            .PaddingBottom(10)
                            .Text("SIA \"FMM\" | Veidlapa V-1 | Versija 1.0 | 29.08.2025")
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken1);

                        page.Content()
                            .Element(container =>
                            {
                                ProductionProtocolPage2.Compose(
                                    container,
                                    _production,
                                    start,
                                    end,
                                    start == 1);
                            });

                        page.Footer()
                            .PaddingTop(10)
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.DefaultTextStyle(x =>
                                    x.FontSize(9)
                                    .FontColor(Colors.Grey.Darken1));

                                text.Span("Lapa ");

                                text.CurrentPageNumber();

                                text.Span(" no ");

                                text.TotalPages();
                            });
                    });
                }
        }
}