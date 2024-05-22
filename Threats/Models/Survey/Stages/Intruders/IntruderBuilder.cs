using Threats.Data.Entities;

namespace Threats.Models.Survey.Stages.Intruders;

public class IntruderBuilder
{
    public IntruderBuilder(IntruderData data)
    {
        Data = data;
    }

    public IntruderData Data { get; }
    public IntruderType Type { get; private set; }

    public void SetType(IntruderType type)
    {
        Type = type;
    }

    public Intruder Build() => new(Type, Data.Potential);
}
