using System;
using System.Collections.Generic;

namespace Threats.Data.Entities;

public class Threat : Entity
{
    public Threat(
        int id,
        string name,
        string description,
        SafetyViolations violations,
        IReadOnlyList<Intruder> intruders,
        IReadOnlyList<Object> objects,
        DateOnly addDate,
        DateOnly updateDate) : base(id)
    {
        Name = name;
        Description = description;
        Violations = violations;
        Intruders = intruders;
        Object = objects;
        AddDate = addDate;
        UpdateDate = updateDate;
    }

    public string Name { get; }
    public string Description { get; }
    public SafetyViolations Violations { get; }

    public IReadOnlyList<Intruder> Intruders { get; }
    public IReadOnlyList<Object> Object { get; }

    public DateOnly AddDate { get; }
    public DateOnly UpdateDate { get; }
}
