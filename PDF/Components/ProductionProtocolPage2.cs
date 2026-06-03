using ISO_ERP.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ISO_ERP.PDF.Components;

public static class ProductionProtocolPage2
{
    public static void Compose(
            IContainer container,
            Production production,
            int startIndex,
            int endIndex,
            bool showTopInfo)
    {
        container.Column(column =>
        {
            column.Spacing(4);

            // TITLE

            column.Item()
                .AlignCenter()
                .Text("KOMPLEKTĒŠANA - III posms")
                .FontSize(14)
                .Bold();

            // TOP INFO AREA

                if (showTopInfo)
{
                column.Item()
                    .PaddingBottom(2)
                    .Row(row =>
                    {
                        row.Spacing(25);

                        void InfoLine(string label)
                        {
                            row.RelativeItem()
                                .Row(inner =>
                                {
                                    inner.ConstantItem(48)
                                        .Text($"{label}:")
                                        .Bold()
                                        .FontSize(11);

                                    inner.RelativeItem()
                                        .PaddingLeft(0)
                                        .BorderBottom(0.7f)
                                        .BorderColor(Colors.Grey.Medium)
                                        .Height(16);
                                });
                        }

                        InfoLine("Datums");
                        InfoLine("Uzvārds");
                        InfoLine("Paraksts");
                    });
}
            // MAIN TABLE

            column.Item()
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(24);
                        columns.RelativeColumn(1.3f);
                        columns.RelativeColumn(2f);
                        columns.ConstantColumn(30);
                        columns.RelativeColumn(2f);
                    });

                    void HeaderCell(string text)
                    {
                        table.Cell()
                            .Background("#d9d9d9")
                            .Border(1)
                            .Padding(5)
                            .AlignCenter()
                            .AlignMiddle()
                            .Text(text)
                            .Bold()
                            .FontSize(10);
                    }

                    int rowHeight;

                            if (production.Quantity <= 43)
                            {
                                rowHeight = 16;
                            }
                            else
                            {
                                rowHeight = 18;
                            }

                    void BodyCell(string text = "", bool isCupCode = false)
                        {
                            var textStyle = table.Cell()
                                .Border(1)
                                .MinHeight(rowHeight)
                                .Padding(1)
                                .AlignMiddle()
                                .AlignCenter();

                            var txt = textStyle.Text(text);

                            if (isCupCode)
                            {
                                txt.FontSize(12)
                                .Bold()
                                .SemiBold();
                            }
                            else
                            {
                                txt.FontSize(10);
                            }
                        }

                    table.Header(header =>
                        {
                            HeaderCell("Nr.");
                            HeaderCell("Kausa Nr.");
                            HeaderCell("Cilindra Nr.");
                            HeaderCell("OK");
                            HeaderCell("Piezīmes");
                        });

                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        BodyCell(i.ToString());

                        BodyCell($"{production.BatchNo}{production.ProductCode}{i}", true);

                        BodyCell();

                        table.Cell()
                            .Border(1)
                            .MinHeight(rowHeight)
                            .AlignCenter()
                            .AlignMiddle()
                            .Element(x =>
                            {
                                x.Width(14)
                                    .Height(14)
                                    .Border(1.5f);
                            });

                        BodyCell();
                    }
                });
        });
    }
}