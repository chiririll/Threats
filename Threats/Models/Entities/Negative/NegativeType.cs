namespace Threats.Models.Entities
{
    public class NegativeType(int id, string name) : Entity(id)
    {
        public string Name { get; } = name;
    }
}
