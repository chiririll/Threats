using System;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public abstract class Entity : IComparable<Entity>, IEquatable<Entity>
{
    public Entity(int id)
    {
        Id = id;
    }

    [JsonProperty("id")] public int Id { get; }

    public int CompareTo(Entity? other) => other == null ? 1 : Id.CompareTo(other.Id);
    public bool Equals(Entity? other) => other != null && Id.Equals(other.Id);
}
