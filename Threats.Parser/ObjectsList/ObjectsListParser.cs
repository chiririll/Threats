using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace Threats.Parser.ObjectsList;

public class ObjectsListParser : IParser
{
    private const int HeaderRowsCount = 1;
    private const string EmptyObjectsString = "новые объекты не добавляются";

    private readonly string path;

    private ParsedData? data;

    public ObjectsListParser(string path)
    {
        this.path = path;
    }

    public void Init(ParsedData data)
    {
        this.data = data;
    }

    public void Parse()
    {
        var stream = File.Open(path, FileMode.Open, FileAccess.Read);
        var reader = ExcelReaderFactory.CreateReader(stream);

        var data = reader.AsDataSet();

        var objectsTable = data.Tables[0];
        ParseObjects(objectsTable);

        reader.Close();
        stream.Close();
    }

    private void ParseObjects(DataTable table)
    {
        var questions = new List<QuestionBuilder>();

        foreach (var row in table.AsEnumerable().Skip(HeaderRowsCount))
        {
            var idString = row.Field<string>(Columns.Id);

            if (!string.IsNullOrWhiteSpace(idString))
            {
                var id = System.Convert.ToInt32(idString.Split('.').First().Trim());
                questions.Add(new QuestionBuilder(id));
            }

            var question = questions[^1];

            question.AddTitle(row.Field<string>(Columns.QuestionTitle));
            question.AddHelpText(row.Field<string>(Columns.HelpText));

            var option = row.Field<string>(Columns.Option);
            if (!string.IsNullOrWhiteSpace(option))
            {
                question.AddOption(option);
            }

            foreach (var objectId in TryParseObjects(row.Field<string>(Columns.Object)))
            {
                question.AddOptionObject(objectId);
            }
        }

        data!.objectsQuestion.AddRange(questions.Select(q => q.Build()));
    }

    private IEnumerable<int> TryParseObjects(string? str)
    {
        var objects = new List<int>();

        var objectString = str?.Trim() ?? null;
        if (string.IsNullOrWhiteSpace(objectString))
        {
            return objects;
        }

        foreach (var obj in objectString.Split(';'))
        {
            if (!TryParseObject(obj, out int id))
                continue;

            objects.Add(id);
        }

        return objects;
    }

    private bool TryParseObject(string objectString, out int id)
    {
        id = default;

        var lowered = objectString.Trim().ToLower();
        if (lowered.Equals(EmptyObjectsString))
        {
            return false;
        }

        var obj = data!.objects.Find(o => o.Name.ToLower().Equals(lowered));
        if (obj == null)
        {
            Trace.TraceError($"Object \"{objectString}\" not found!");
            return false;
        }

        id = obj.Id;
        return true;
    }

    private static class Columns
    {
        public const int Id = 0;
        public const int QuestionTitle = 1;
        public const int HelpText = 2;
        public const int Option = 3;
        public const int Object = 4;
    }
}
