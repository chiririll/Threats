using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Models.Survey.Data;

public class DummySurveyData : ISurveyData
{
    public string TitleFormat { get; } = "Этап {0}: {1}";

    public INegativesStepData NegativesStepData { get; } = new DummyNegativesData();
    public IThreatsStepData ThreatsStepData { get; } = new DummyThreatsData();

}

public class DummyNegativesData : INegativesStepData
{
    private readonly List<NegativeType> types;

    private readonly List<Negative> negatives;

    public DummyNegativesData()
    {
        types = new()
        {
            new(1, "Ущерб физическому лицу (У1):"),
            new(2, "Ущерб юридическому лицу, индивидуальному предпринимателю, связанный с хозяйственной деятельностью (У2)"),
            new(3, "Ущерб государству (У3)"),
        };

        negatives = new()
        {
            new(1, "Negative 1 (type 1)", types[0]),
            new(2, "Negative 2 (type 1)", types[0]),
            new(3, "Negative 3 (type 1)", types[0]),
            new(4, "Negative 4 (type 2)", types[1]),
            new(5, "Negative 5 (type 2)", types[1]),
            new(6, "Negative 6 (type 2)", types[1]),
            new(7, "Negative 7 (type 3)", types[2]),
            new(8, "Negative 8 (type 3)", types[2]),
            new(9, "Negative 9 (type 3)", types[2]),
        };
    }

    public string Title => "Определение негативных последствий";

    public string TypesQuestionLabel => "Выберите актуальные виды рисков (ущерба), которые могут наступить от нарушения или прекращения основных процессов в системе:";

    public IEnumerable<NegativeType> NegativeTypes => types;


    public string NegativesQuestionLabel => "Выберите негативные последствия, которые могут наступить в результате реализации (возникновения) угроз безопасности информации и привести к выбранным ранее видам рисков (ущерба):";

    public IEnumerable<Negative> Negatives => negatives;
}

public class DummyThreatsData : IThreatsStepData
{
    public string Title => "Threats step";
}
