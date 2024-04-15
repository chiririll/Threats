using System.Collections.Generic;
using System.Linq;
using Threats.Models.Entities;

namespace Threats.Models.Questions
{
    public class NegativeTypeQuestion : IMultipleChoiceQuestion
    {
        private List<Option> options;
        private List<Option> selected;

        public NegativeTypeQuestion(IReadOnlyList<NegativeType> types)
        {
            options = types.Select(t => new Option(t.Id, t.Name)).ToList();
            selected = new();
        }

        public string Label => "Negative Types:";

        public IEnumerable<Option> Options => options;
        public IEnumerable<Option> Selected => selected;
    }
}
