using System.Diagnostics;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Threats.ViewModels.Alerts;

namespace Threats.Views.Alerts;

public partial class ExportResultAlert : Window
{
    public ExportResultAlert()
    {
        InitializeComponent();
    }

    public void Close(object sender, RoutedEventArgs args) => Close();

    public void ShowResult(object sender, RoutedEventArgs args)
    {
        if (DataContext is not ExportResultViewModel result || !result.Success)
        {
            return;
        }

        try
        {
            var workDir = Directory.GetParent(result.Path)?.FullName ?? result.Path;

            var processInfo = new ProcessStartInfo()
            {
                FileName = result.Path,
                WorkingDirectory = workDir,
                UseShellExecute = true,
            };

            Process.Start(processInfo);
        }
        catch (System.Exception)
        {
        }
    }
}
