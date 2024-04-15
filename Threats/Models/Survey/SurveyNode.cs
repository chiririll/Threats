using System.Collections.Generic;
using Threats.Models.Questions;

namespace Threats.Models
{
    public abstract class SurveyNode : Node
    {
        protected SurveyNode(List<Node> children) : base(children)
        {
        }

        public abstract IQuestion GetQuestion();
    }
}
