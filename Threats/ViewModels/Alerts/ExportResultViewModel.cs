namespace Threats.ViewModels.Alerts;

public class ExportResultViewModel : ViewModelBase
{
    private const string SuccessMessage = "Результат успешно экспортирован!";
    private const string FailMessage = "Во время экспорта произошла ошибка!";

    public ExportResultViewModel()
    {
        Success = false;
        Path = string.Empty;

        Message = Success ? SuccessMessage : FailMessage;
    }

    public ExportResultViewModel(string path)
    {
        Success = true;
        Path = path;

        Message = Success ? SuccessMessage : FailMessage;
    }

    public bool Success { get; }
    public string Path { get; }

    public string Message { get; }
}
