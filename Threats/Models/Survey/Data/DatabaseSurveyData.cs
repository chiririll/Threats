using Threats.Data.Database;

namespace Threats.Models.Survey.Data;

public class DatabaseSurveyData : DummySurveyData
{
    public DatabaseSurveyData()
    {
        var db = new DatabaseConnection("../DataProcessor/threats.db");
        db.Load();
    }
}
