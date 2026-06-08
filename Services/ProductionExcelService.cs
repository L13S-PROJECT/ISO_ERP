using ClosedXML.Excel;
using ISO_ERP.Models;

namespace ISO_ERP.Services;

public class ProductionExcelService
{
    public byte[] GenerateExcel(List<Production> productions)
    {
        using var workbook = new XLWorkbook();

        var ws = workbook.AddWorksheet("Ražošana");

            ws.Cell("A1").Value = "RAŽOŠANAS UZSKAITES TABULA (R-1)";

            ws.Range("A1:J1").Merge();

            ws.Cell("A1").Style.Font.Bold = true;

            ws.Cell("A1").Style.Font.FontSize = 16;
            
            ws.Cell("A1").Style.Alignment.Vertical =
                XLAlignmentVerticalValues.Center;

            ws.Row(1).Height = 30;

            ws.Cell("A1").Style.Alignment.Horizontal =
                XLAlignmentHorizontalValues.Center;
            ws.Cell("A3").Value = "Raž.Partijas Nr.";
            ws.Cell("B3").Value = "Kausa kods";
            ws.Cell("C3").Value = "Kods";
            ws.Cell("D3").Value = "Skaits";
            ws.Cell("E3").Value = "WPS";
            ws.Cell("F3").Value = "Sākuma datums";
            ws.Cell("G3").Value = "Pabeigts";
            ws.Cell("H3").Value = "Pārbaudīja";
            ws.Cell("I3").Value = "Piezīmes";
            ws.Cell("J3").Value = "Printēts";

            ws.Range("A3:J3").Style.Font.Bold = true;

            ws.Range("A1:J1").Style.Fill.BackgroundColor =
                XLColor.Yellow;

            ws.Range("A3:J3").Style.Fill.BackgroundColor =
                XLColor.LightGray;

            ws.Range("A3:J3").Style.Border.OutsideBorder =
                XLBorderStyleValues.Thin;

            ws.Range("A3:J3").Style.Border.InsideBorder =
                XLBorderStyleValues.Thin;

            ws.Range("A3:J3").SetAutoFilter();
            var row = 4;

            var sortedProductions = productions
                .OrderBy(p => new string(p.BatchNo.TakeWhile(char.IsLetter).ToArray()))
                .ThenBy(p =>
                {
                    var num = new string(p.BatchNo.SkipWhile(char.IsLetter).ToArray());
                    return int.TryParse(num, out var n) ? n : 0;
                })
                .ThenBy(p => p.StartDate)
                .ToList();

            foreach (var item in sortedProductions)
            {
                ws.Cell(row, 1).Value = item.BatchNo;
                ws.Cell(row, 2).Value = item.ProductName;
                ws.Cell(row, 3).Value = item.ProductCode;
                ws.Cell(row, 4).Value = item.Quantity;
                ws.Cell(row, 5).Value = item.Recipe;
                ws.Cell(row, 6).Value = item.StartDate;
                ws.Cell(row, 6).Style.DateFormat.Format = "dd.MM.yyyy";

                ws.Cell(row, 7).Value = item.FinishedDate;
                ws.Cell(row, 7).Style.DateFormat.Format = "dd.MM.yyyy";
                ws.Cell(row, 8).Value = item.CheckedBy;
                ws.Cell(row, 9).Value = item.Notes;
                ws.Cell(row, 10).Value = item.IsPrinted ? "✓" : "";

                row++;
            }
            
        ws.Columns().AdjustToContents();
        ws.SheetView.FreezeRows(3);

        ws.PageSetup.PageOrientation =
                XLPageOrientation.Landscape;

        var usedRange = ws.RangeUsed();

            if (usedRange != null)
            {
                usedRange.Style.Border.OutsideBorder =
                    XLBorderStyleValues.Thin;

                usedRange.Style.Border.InsideBorder =
                    XLBorderStyleValues.Thin;
            }

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}