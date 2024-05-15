using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Data;

public interface IEntitiesData
{
    public IReadOnlyList<Threat> Threats { get; }
    public IReadOnlyList<Object> Objects { get; }

    public IReadOnlyList<NegativeType> NegativeTypes { get; }
    public IReadOnlyList<Negative> Negatives { get; }

    public Object? GetObjectById(int id);
}
