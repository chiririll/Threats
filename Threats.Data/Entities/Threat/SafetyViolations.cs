using System;

namespace Threats.Data.Entities;

[Flags]
public enum SafetyViolations
{
    None = 0,

    Availability = 1,
    Integrity = Availability << 1,
    Privacy = Integrity << 1,
}
