using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Threats.Data.Entities;

namespace Threats.ViewModels.Survey;

public class ScriptTypeViewModel : ViewModelBase
{
    public ScriptTypeViewModel(ScriptType data, IEnumerable<Script> scripts)
    {
        Id = data.ToString();
        ScriptType = data;

        Scripts = new(scripts.Select(s => new ScriptViewModel(s)));
    }

    public string Id { get; }
    public ScriptType ScriptType { get; }
    public bool HasNote => ScriptType.Note != null;

    public ObservableCollection<ScriptViewModel> Scripts { get; }
}
