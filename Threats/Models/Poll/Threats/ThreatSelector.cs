using Threats.Models.Questions;

namespace Threats.Models.Poll;

public class ThreatSelector : Selector
{
    private IntruderSelector intruderSelector;
    private ObjectSelector objectSelector;

    public override Question GetQuestion()
    {
        throw new System.NotImplementedException();
    }

    public override void SetResult(Question result)
    {
        throw new System.NotImplementedException();
    }
}
