using Threats.Models.Survey.Data.Base;

namespace Threats.Models.Survey.Data;

public class ObjectsStageData : StageData, IObjectsStageData
{
    public ObjectsStageData(string title) : base(title)
    {
    }
}
