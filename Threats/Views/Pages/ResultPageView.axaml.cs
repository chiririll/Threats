using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Threats.ViewModels.Alerts;
using Threats.ViewModels.Pages;
using Threats.Views.Alerts;

namespace Threats.Views.Pages;

public partial class ResultPageView : UserControl
{
    private ResultPageViewModel? viewModel;

    public ResultPageView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        viewModel = DataContext as ResultPageViewModel;
    }

    private async void ExportResult(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null || viewModel == null)
        {
            return;
        }

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new()
        {
            Title = "Сохранить как",
            FileTypeChoices = viewModel.ExportTypes,
            SuggestedFileName = "threats",
            ShowOverwritePrompt = true,
        });

        if (file == null)
        {
            return;
        }

        var path = file.Path.AbsolutePath;
        if (viewModel.Export(path))
        {
            ShowResultAlert(new(path));
            return;
        }

        ShowResultAlert(new());
    }

    private async void ShowResultAlert(ExportResultViewModel result)
    {
        if (TopLevel.GetTopLevel(this) is not Window window)
        {
            return;
        }

        var alert = new ExportResultAlert
        {
            DataContext = result
        };

        await alert.ShowDialog(window);
    }
}