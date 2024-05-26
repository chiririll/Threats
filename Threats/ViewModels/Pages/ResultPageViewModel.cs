using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using Avalonia.Platform.Storage;
using ReactiveUI;
using Threats.Models.Exporters;
using Threats.Models.Exporters.Excel;
using Threats.Models.Survey;

namespace Threats.ViewModels.Pages;

public class ResultPageViewModel : ViewModelBase
{
    private readonly Subject<Unit> onRestartRequested = new();

    private readonly List<IResultExporter> exporters;

    public ResultPageViewModel(SurveyResult result)
    {
        Result = result;

        exporters = new()
        {
            new ExcelExporter(),
        };

        ExportResultCommand = ReactiveCommand.Create(() => ExportResult());
        RestartCommand = ReactiveCommand.Create(() => onRestartRequested.OnNext(default));

        ExportTypes = exporters.Select(e => e.OutputType).ToList();
    }

    public SurveyResult Result { get; }

    public IObservable<Unit> ExportResultCommand { get; }
    public IObservable<Unit> RestartCommand { get; }

    public IObservable<Unit> OnRestartRequested => onRestartRequested;

    public IReadOnlyList<FilePickerFileType> ExportTypes { get; }

    private void ExportResult()
    {
        var exporter = new ExcelExporter();

        exporter.Export(Result, "export.xlsx");
    }

    public bool Export(string path)
    {
        var exporter = exporters.FirstOrDefault(e => e.CanExport(path));
        return exporter != null && exporter.Export(Result, path);
    }
}
