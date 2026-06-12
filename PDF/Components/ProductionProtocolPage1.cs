using ISO_ERP.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ISO_ERP.PDF.Components;

public static class ProductionProtocolPage1
{
    public static void Compose(
    IContainer container,
    Production production,
    Product? product)
    {
        container.Column(column =>
        {
            column.Spacing(8);

            // TITLE

            column.Item()
                .AlignCenter()
                .Text("Ražošanas kvalitātes kontroles protokols (FCP)")
                .FontSize(16)
                .Bold();
        
            // DATE

            column.Item()
                    .AlignRight()
                    .Text($"Datums: {production.StartDate:dd.MM.yyyy}")
                    .Bold();
            // TOP TABLE

            column.Item()
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(0.7f);
                            columns.RelativeColumn(0.7f);
                            columns.RelativeColumn(0.5f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(0.9f);
                            columns.RelativeColumn(1.3f);
                        });

                    void HeaderCell(string text)
                    {
                        table.Cell()
                            .Background("#d9d9d9")
                            .Border(1)
                            .Padding(4)
                            .Text(text)
                            .Bold()
                            .FontSize(10);
                    }

                    void ValueCell(string text)
                    {
                        table.Cell()
                            .Border(1)
                            .MinHeight(35)
                            .AlignMiddle()
                            .Padding(4)
                            .AlignCenter()
                            .Text(text)
                            .FontSize(14)
                            .Bold();
                    }

                    HeaderCell("Ražošanas kods");
                    HeaderCell("Kausa kods");
                    HeaderCell("Skaits");
                    HeaderCell("Rasējuma Nr.");
                    HeaderCell("Pabeigts");
                    HeaderCell("Pārbaudīja");

                    // ValueCell(production.BatchNo ?? "");
                    ValueCell("ABC123");
                    ValueCell(production.ProductCode ?? "");
                    ValueCell(production.Quantity.ToString());
                    ValueCell("");
                    ValueCell("");
                    ValueCell("");
                });

            // COMMENT LINES

            column.Item()
                .PaddingTop(16);

                for (int i = 0; i < 3; i++)
                {
                    column.Item()
                        .PaddingBottom(12)
                        .LineHorizontal(1)
                        .LineColor(Colors.Grey.Lighten1);
                }

            
                        // SECTION TITLE

            column.Item()
                .PaddingTop(16)
                .AlignCenter()
                .Text("DETAĻU KOMPLEKTS - I posms")
                .FontSize(12)
                .Bold();

            // STAGE 1 TABLE

            column.Item()
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1.8f);
                    });

                    void HeaderCell(string text)
                    {
                        table.Cell()
                            .Background("#d9d9d9")
                            .Border(1)
                            .Padding(5)
                            .Text(text)
                            .Bold()
                            .FontSize(11);
                    }

                    void EmptyCell(bool light = false)
                        {
                            table.Cell()
                                .BorderTop(light ? 0.5f : 1)
                                .BorderBottom(light ? 0.5f : 1)
                                .BorderLeft(light ? 0.5f : 1)
                                .BorderRight(light ? 0.5f : 1)
                                .BorderColor(light ? "#cfcfcf" : "#000000")
                                .Height(24);
                        }

                    HeaderCell("Datums");
                    HeaderCell("Komplekts");
                    HeaderCell("Paraksts");
                    HeaderCell("Piezīmes");

                    var detailRows = product?.ProductDetails ?? new();

                        for (int i = 0; i < 7; i++)
                        {
                            var detail = i < detailRows.Count
                                ? detailRows[i]
                                : null;

                            EmptyCell(detail == null);

                            table.Cell()
                                .BorderTop(detail == null ? 0.5f : 1)
                                .BorderBottom(detail == null ? 0.5f : 1)
                                .BorderLeft(detail == null ? 0.5f : 1)
                                .BorderRight(detail == null ? 0.5f : 1)
                                .BorderColor(detail == null ? "#cfcfcf" : "#000000")
                                .Height(24)
                                .PaddingLeft(4)
                                .AlignMiddle()
                                .Text(detail?.Detail?.Name ?? "");

                            EmptyCell(detail == null);
                            EmptyCell(detail == null);

                        }
                });
    

                        // SECTION TITLE

            column.Item()
                .PaddingTop(16)
                .AlignCenter()
                .Text("METINĀŠANA - II posms")
                .FontSize(12)
                .Bold();

            // WELDING TABLE

            column.Item()
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(35);
                        columns.RelativeColumn(1.3f);
                        columns.RelativeColumn(2.2f);
                        columns.RelativeColumn(1.4f);
                        columns.RelativeColumn(1.4f);
                        columns.RelativeColumn(1.4f);
                    });

                    void HeaderCell(string text)
                    {
                        table.Cell()
                            .Background("#d9d9d9")
                            .Border(1)
                            .Padding(5)
                            .AlignMiddle()
                            .Text(text)
                            .Bold()
                            .FontSize(10);
                    }

                    HeaderCell("Nr.");
                    HeaderCell("Metināma detaļa");
                    HeaderCell("Metinātājs (Uzvārds)");
                    HeaderCell("Iesākts");
                    HeaderCell("Pabeigts");
                    HeaderCell("Paraksts");

                    var detailRows = product?.ProductDetails ?? new();


                                for (int i = 0; i < detailRows.Count; i++)
                                {
                                    var detail = detailRows[i];

                                    void StyledCell(string text = "")
                                    {
                                        table.Cell()
                                            .Border(1)
                                            .MinHeight(24)
                                            .Padding(4)
                                            .AlignMiddle()
                                            .Text(text)
                                            .FontSize(9);
                                    }

                                    StyledCell((i + 1).ToString());
                                    StyledCell(detail?.Detail?.Name ?? "");
                                    StyledCell();
                                    StyledCell();
                                    StyledCell();
                                    StyledCell();
                                }
                        
                });

        });
    }
}