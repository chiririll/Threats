using System.IO;
using System.Linq;
using Avalonia.Platform.Storage;
using NPOI.XSSF.UserModel;
using Threats.Data;
using Threats.Models.Survey;

namespace Threats.Models.Exporters.Excel;

public class ExcelExporter(IEntitiesData entities) : IResultExporter
{
    private readonly IEntitiesData entities = entities;

    public FilePickerFileType OutputType => new("Excel file")
    {
        Patterns = ["*.xlsx"],
        MimeTypes = ["application/vnd.ms-excel"]
    };

    public bool CanHandlePath(string path) => OutputType.Patterns!.Any(p => path.EndsWith(p.Substring(1)));

    public bool Export(SurveyResult result, string path)
    {
        var templateStream = DataLoader.GetExportExcelTemplate();
        var workbook = new XSSFWorkbook(templateStream);
        templateStream.Close();

        var threats = new ThreatsSheetCreator(workbook, entities);
        threats.Create(result);

        try
        {
            var outputStream = File.OpenWrite(path);
            workbook.Write(outputStream, false);
            outputStream.Close();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}
