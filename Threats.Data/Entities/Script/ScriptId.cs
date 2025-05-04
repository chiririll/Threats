using System;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public readonly struct ScriptId : IEquatable<ScriptId>
{
    public ScriptId(ushort type, ushort id)
    {
        Type = type;
        Id = id;
    }

    [JsonProperty("type")] public ushort Type { get; }
    [JsonProperty("id")] public ushort Id { get; }

    public static bool TryParse(string identifier, out ScriptId scriptId)
    {
        scriptId = default;
        var parts = identifier.Split(['.'], 2);

        if (parts.Length < 2 || parts[0].Length < 2)
        {
            return false;
        }

        if (!ushort.TryParse(parts[0].Substring(1), out var type) || !ushort.TryParse(parts[1], out var id))
        {
            return false;
        }

        scriptId = new(type, id);
        return true;
    }

    public override string ToString() => $"Т{Type}.{Id}";

    #region Equals

    public bool Equals(ScriptId other) => Type.Equals(other.Type) && Id.Equals(other.Id);
    public override bool Equals(object? obj) => obj != null && obj is ScriptId other && Equals(other);

    public override int GetHashCode() => (Type << 16) + Id;

    public static bool operator ==(ScriptId left, ScriptId right) => left.Equals(right);

    public static bool operator !=(ScriptId left, ScriptId right) => !(left == right);

    #endregion Equals
}
