using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
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
            return;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new()
        {
            Title = "Сохранить как",
            FileTypeChoices = viewModel.ExportTypes,
            SuggestedFileName = "threats",
            ShowOverwritePrompt = true,
        });

        if (file == null)
            return;

        var path = file.Path.AbsolutePath;
        if (viewModel.Export(path))
        {
            await ShowSuccessAlert(topLevel);
        }
        else
        {
            await MessageBoxManager
                .GetMessageBoxStandard("Ошибка", "Во время экспорта произошла ошибка!")
                .ShowAsPopupAsync(topLevel);
        }
    }

    private async Task ShowSuccessAlert(ContentControl topLevel)
    {
        const string showButtonName = "Посмотреть";
        const string closButtonName = "Закрыть";

        var messageBoxParams = new MessageBoxCustomParams()
        {
            ContentTitle = "Информация",
            ContentMessage = "Результат успешно экспортирован!",

            ButtonDefinitions = [
                new() { Name = showButtonName, IsDefault = true },
                new() { Name = closButtonName, IsCancel = true }
            ],
        };

        var result = await MessageBoxManager
            .GetMessageBoxCustom(messageBoxParams)
            .ShowAsPopupAsync(topLevel);

        if (result == showButtonName)
            viewModel?.ShowExportedResult();
    }
}