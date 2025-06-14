using System;
using System.Linq;
using System.Reactive.Disposables;
using Avalonia.Controls;
using MsBox.Avalonia;
using Threats.ViewModels.Survey;

namespace Threats.Views.Survey;

public partial class ObjectsStageView : UserControl, IDisposable
{
    private readonly CompositeDisposable disp = new();

    private ObjectsStageViewModel? viewModel;

    public ObjectsStageView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        disp.Clear();
        base.OnDataContextChanged(e);

        viewModel = DataContext as ObjectsStageViewModel;
        viewModel?.OnImportRequested
            .Subscribe(_ => ImportObjects())
            .AddTo(disp);
    }

    public async void ImportObjects()
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null || viewModel == null)
            return;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new()
        {
            Title = "Импорт объектов воздействия",
        });

        if (files == null || files.Count < 1)
            return;

        var paths = files.Where(f => f != null).Select(f => f.Path.AbsolutePath);
        if (viewModel.ImportFiles(paths))
            return;

        await MessageBoxManager
            .GetMessageBoxStandard("Ошибка", "Ошибка импорта списка объектов")
            .ShowAsPopupAsync(topLevel);
    }

    public void Dispose()
    {
        disp.Dispose();
    }
}