using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Threats.ViewModels.Pages;

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

        if (file != null && viewModel.Export(file.Path.AbsolutePath))
        {
            // Success
            return;
        }

        // TODO: Show Error alert
    }
}