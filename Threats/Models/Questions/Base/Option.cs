namespace Threats.Models.Questions
{
    public class Option
    {
        public Option(int id, string label)
        {
            Id = id;
            Label = label;
        }

        public int Id { get; }
        public string Label { get; }
    }
}
