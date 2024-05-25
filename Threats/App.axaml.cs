using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Threats.ViewModels;
using Threats.Views;

namespace Threats;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        InitStrings();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void InitStrings()
    {
        if (!Current!.TryGetResource("strings", ThemeVariant.Default, out var stringsResource)
            || stringsResource is not IResourceProvider strings)
        {
            return;
        }

        Current.Resources.MergedDictionaries.Add(strings);
    }
}
