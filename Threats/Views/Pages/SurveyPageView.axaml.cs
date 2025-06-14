using System;
using System.Reactive.Disposables;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Threats.ViewModels.Pages;

namespace Threats.Views.Pages;

public partial class SurveyPageView : UserControl, IDisposable
{
    private readonly CompositeDisposable disp = new();

    private SurveyPageViewModel? viewModel;

    public SurveyPageView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        disp.Clear();
        base.OnDataContextChanged(e);

        viewModel = DataContext as SurveyPageViewModel;
        viewModel?.OnCompletionRequested
            .Subscribe(_ => ConfirmCompletion())
            .AddTo(disp);
    }

    private async void ConfirmCompletion()
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null || viewModel == null)
            return;

        var result = await MessageBoxManager
           .GetMessageBoxStandard(
                "Подтверждение",
                "Вы готовы сформировать список актуальных угроз?",
               ButtonEnum.YesNo)
            .ShowAsPopupAsync(topLevel);

        if (result != ButtonResult.Yes)
            return;

        viewModel.CompleteSurvey();
    }

    public void Dispose()
    {
        disp.Dispose();
    }
}
