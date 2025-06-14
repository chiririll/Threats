using System.Collections.Generic;
using System.IO;
using System.Linq;
using Threats.Data;
using Threats.Data.Entities;

namespace Threats.Models.Objects.Importers
{
    public class PlainTextImporter : BaseObjectsImporter
    {
        protected override bool Import(string file, IEntitiesData entities, ref List<Object> result)
        {
            try
            {
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    var obj = entities.Objects.First(o => o.Name.Equals(line, System.StringComparison.InvariantCultureIgnoreCase));
                    if (obj == null)
                        continue;

                    result.Add(obj);
                }
                return true;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            return false;
        }
    }
}