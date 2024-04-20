using System;
using System.Collections.Generic;

namespace Threats.Models.Entities;

public class Threat(
    int id,
    string name,
    string description,
    SafetyViolations violations,
    IReadOnlyList<Intruder> intruders,
    IReadOnlyList<Object> objects,
    DateOnly addDate,
    DateOnly updateDate) : Entity(id)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public SafetyViolations Violations { get; } = violations;

    public IReadOnlyList<Intruder> Intruders { get; } = intruders;
    public IReadOnlyList<Object> Object { get; } = objects;

    public DateOnly AddDate { get; } = addDate;
    public DateOnly UpdateDate { get; } = updateDate;
}
