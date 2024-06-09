using Avalonia.Platform.Storage;
using Threats.Models.Survey;

namespace Threats.Models.Exporters;

public interface IResultExporter
{
    public FilePickerFileType OutputType { get; }

    public bool CanHandlePath(string path);
    public bool Export(SurveyResult result, string path);
}
