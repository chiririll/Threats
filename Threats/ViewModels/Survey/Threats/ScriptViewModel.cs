using Threats.Data.Entities;

namespace Threats.ViewModels.Survey;

public class ScriptViewModel : ViewModelBase
{
    public ScriptViewModel(Script script)
    {
        Id = script.Id.ToString();
        Script = script;
    }

    public string Id { get; }
    public Script Script { get; }

    public bool HasExamples => Script.Examples.Count > 0;
    public bool HasNote => Script.Note != null;
}
