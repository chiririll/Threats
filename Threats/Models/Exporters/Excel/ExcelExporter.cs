using System.IO;
using NPOI.XSSF.UserModel;
using Threats.Data;
using Threats.Models.Survey;

namespace Threats.Models.Exporters.Excel;

public class ExcelExporter : IResultExporter
{
    public void Export(SurveyResult result, string path)
    {
        var templateStream = DataLoader.GetExportExcelTemplate();
        var workbook = new XSSFWorkbook(templateStream);
        templateStream.Close();

        var threats = new ThreatsSheetCreator(workbook);
        threats.Create(result);

        try
        {
            var outputStream = File.OpenWrite(path);
            workbook.Write(outputStream, false);
            outputStream.Close();
        }
        catch (System.Exception)
        {
        }
    }
}
