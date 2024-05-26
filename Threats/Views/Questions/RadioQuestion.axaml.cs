using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Threats.ViewModels.Questions;

namespace Threats.Views.Questions;

public partial class RadioQuestion : UserControl
{
    private QuestionViewModel? viewModel;

    public RadioQuestion()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        viewModel = DataContext as QuestionViewModel;
    }

    private void Updated(object sender, RoutedEventArgs args)
    {
        viewModel?.Updated();
    }
}
