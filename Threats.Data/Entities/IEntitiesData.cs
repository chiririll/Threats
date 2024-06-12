using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Data;

public interface IEntitiesData
{
    public IReadOnlyList<Threat> Threats { get; }
    public IReadOnlyList<Object> Objects { get; }
    public IReadOnlyList<Script> Scripts { get; }

    public IReadOnlyList<NegativeType> NegativeTypes { get; }
    public IReadOnlyList<Negative> Negatives { get; }

    public IReadOnlyList<IntruderData> Intruders { get; }

    public Object? GetObjectById(int id);

    public ScriptType? GetScriptTypeById(int id);
    public Script? GetScriptById(ScriptId id);
}
