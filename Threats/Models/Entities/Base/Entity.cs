namespace Threats.Models.Entities
{
    public abstract class Entity(int id)
    {
        public int Id { get; } = id;
    }
}
