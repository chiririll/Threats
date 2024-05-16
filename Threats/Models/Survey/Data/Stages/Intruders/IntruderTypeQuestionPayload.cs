using Threats.Data.Entities;
using Threats.Data.Questions;

namespace Threats.Models.Survey.Data;

public class IntruderTypePayload : IOptionPayload
{
    public IntruderTypePayload(IntruderType type)
    {
        Type = type;
    }

    public IntruderType Type { get; }
}
