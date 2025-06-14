using System.Collections.Generic;
using Threats.Data;
using Threats.Data.Entities;

namespace Threats.Models.Objects.Importers
{
    public interface IObjectsImporter
    {
        public void Init(IEntitiesData entities);
        public bool Import(string file, ref List<Object> result);
    }
}