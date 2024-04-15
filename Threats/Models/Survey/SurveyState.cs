using System.Collections.Generic;
using Threats.Models.Questions;

namespace Threats.Models
{
    public class SurveyState
    {
        private readonly List<SurveyNode> nodes;
        private readonly Stack<SurveyNode> path = new();

        public SurveyState()
        {
            nodes = new()
            {
                // TODO
            };
        }

        public IQuestion GetQuestion()
        {
            throw new System.NotImplementedException();
        }

        public void GetResult()
        {
            throw new System.NotImplementedException();
        }
    }
}
