using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Threats.Data;
using Threats.Data.Entities;
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
            CreateCell(row, Columns.Intruders, GetIntrudersString(threat.Intruders));
            CreateCell(row, Columns.Objects, GetObjectsString(threat.ObjectIds));
            CreateCell(row, Columns.Scripts, string.Join("; ", threat.ScriptIds));
        }
    }

    private void CreateCell(IRow row, int index, string value)
    {
        var cell = row.CreateCell(index);
        cell.SetCellValue(value);
        cell.CellStyle = cellStyle;
    }

    private string GetIntrudersString(IReadOnlyList<Intruder> intruders)
    {
        var sb = new StringBuilder();

        foreach (var intruder in intruders)
        {
            var type = intruder.Type switch
            {
                IntruderType.Internal => "Внутренний",
                IntruderType.External => "Внешний",
                _ => throw new System.NotImplementedException(),
            };
            var potential = intruder.Potential switch
            {
                IntruderPotential.Base => "c базовым",
                IntruderPotential.Advanced => "с базовым повышенным",
                IntruderPotential.Medium => "со средним",
                IntruderPotential.High => "с высоким",
                _ => throw new System.NotImplementedException(),
            };

            sb.Append($"{type} нарушитель {potential} потенциалом; ");
        }

        sb.Remove(sb.Length - 2, 2);
        return sb.ToString();
    }

    private string GetObjectsString(IReadOnlyList<int> objects)
    {
        var strings = objects.Select(id => entities.GetObjectById(id)).Where(o => o != null).Select(o => o!.Name);
        return string.Join("; ", strings);
    }

    private class Columns
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int Description = 2;
        public const int Intruders = 3;
        public const int Objects = 4;
        public const int Scripts = 5;
    }
}
