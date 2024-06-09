using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Threats.Data;
using Threats.Models.Survey;

namespace Threats.Models.Exporters.Excel;

public class ThreatsSheetCreator
{
    private const string SheetName = "Threats";
    private const int StartRow = 2;

    private readonly XSSFWorkbook workbook;
    private readonly IEntitiesData entities;

    private readonly ICellStyle cellStyle;

    public ThreatsSheetCreator(XSSFWorkbook workbook, IEntitiesData entities)
    {
        this.workbook = workbook;
        this.entities = entities;

        cellStyle = workbook.CreateCellStyle();
        cellStyle.WrapText = true;
        cellStyle.VerticalAlignment = VerticalAlignment.Center;

        cellStyle.BorderBottom = BorderStyle.Thin;
        cellStyle.BorderRight = BorderStyle.Thin;
    }

    public void Create(SurveyResult result)
    {
        var sheet = workbook.GetSheet(SheetName);
        if (sheet == null)
            return;

        var rowIndex = StartRow;
        foreach (var threat in result.Threats)
        {
            var row = sheet.CreateRow(rowIndex++);
            CreateCell(row, Columns.Id, threat.Id.ToString());
            CreateCell(row, Columns.Name, threat.Name);
            CreateCell(row, Columns.Description, threat.Description);
        }
    }

    private void CreateCell(IRow row, int index, string value)
    {
        var cell = row.CreateCell(index);
        cell.SetCellValue(value);
        cell.CellStyle = cellStyle;
    }

    private class Columns
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int Description = 2;
        public const int Intruders = 3;
        public const int Objects = 4;
    }
}
