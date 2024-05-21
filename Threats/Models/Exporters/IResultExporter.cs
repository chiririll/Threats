using Threats.Models.Survey;

namespace Threats.Models.Exporters;

public interface IResultExporter
{
    public void Export(SurveyResult result, string path);
}
