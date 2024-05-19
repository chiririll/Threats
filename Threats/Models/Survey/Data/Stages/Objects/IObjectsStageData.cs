namespace Threats.Models.Survey.Data;

public interface IObjectsStageData : IStageData
{
    public string Header { get; }

    public string ObjectResultsHeader { get; }

    public string AddObjectHeader { get; }
    public string AddObjectButton { get; }
}
