using System;
using System.Reactive.Disposables;
using Avalonia.Controls;
using Threats.ViewModels.Survey;

namespace Threats.Views.Survey;

public partial class ObjectsStageView : UserControl, IDisposable
{
    private readonly CompositeDisposable disp = new();
    private readonly ObjectsStageViewModel? viewModel;

    public ObjectsStageView()
    {
        InitializeComponent();

        viewModel = DataContext as ObjectsStageViewModel;
    }

    public void ImportObjects()
    {
    }

    public void Dispose()
    {
        disp.Dispose();
    }
}