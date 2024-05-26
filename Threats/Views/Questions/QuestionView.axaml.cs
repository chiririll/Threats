using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Threats.ViewModels.Questions;

namespace Threats.Views.Questions;

public partial class QuestionView : UserControl
{
    private QuestionViewModel? viewModel;

    public QuestionView()
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
        Console.WriteLine("Updated");
        viewModel?.Updated();
    }
}
