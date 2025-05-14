using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;
using Avalonia.Platform.Storage;
using ReactiveUI;
using Threats.Data;
using Threats.Models.Exporters;
using Threats.Models.Exporters.Excel;
using Threats.Models.Survey;

namespace Threats.ViewModels.Pages;

public class ResultPageViewModel : ViewModelBase
{
    private readonly Subject<Unit> onRestartRequested = new();

    private bool resultExported = false;

    private readonly IResultExporter exporter;

    public ResultPageViewModel(SurveyResult result, IEntitiesData entities)
    {
        Result = result;

        exporter = new ExcelExporter(entities);

        RestartCommand = ReactiveCommand.Create(() => onRestartRequested.OnNext(default));

        ExportTypes = new List<FilePickerFileType>() { exporter.OutputType };
    }

    public bool ResultExported
    {
        get => resultExported;
        private set => this.RaiseAndSetIfChanged(ref resultExported, value);
    }

    public SurveyResult Result { get; }

    public IObservable<Unit> RestartCommand { get; }

    public IObservable<Unit> OnRestartRequested => onRestartRequested;

    public IReadOnlyList<FilePickerFileType> ExportTypes { get; }

    public bool Export(string path)
    {
        var success = exporter.Export(Result, path);
        ResultExported = ResultExported || success;

        return success;
    }
}
