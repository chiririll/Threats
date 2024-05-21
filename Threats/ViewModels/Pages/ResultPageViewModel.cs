using System;
using System.Reactive;
using ReactiveUI;
using Threats.Models.Exporters.Excel;
using Threats.Models.Survey;

namespace Threats.ViewModels.Pages;

public class ResultPageViewModel : ViewModelBase
{
    public ResultPageViewModel(SurveyResult result)
    {
        Result = result;

        ExportResultCommand = ReactiveCommand.Create(() => ExportResult());
    }

    public SurveyResult Result { get; }
    public IObservable<Unit> ExportResultCommand { get; }

    private void ExportResult()
    {
        var exporter = new ExcelExporter();

        exporter.Export(Result, "export.xlsx");
    }
}
