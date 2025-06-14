using System.Collections.Generic;
using Threats.Data;
using Threats.Data.Entities;

namespace Threats.Models.Objects.Importers
{
    public abstract class BaseObjectsImporter : IObjectsImporter
    {
        private IEntitiesData? entities;

        public void Init(IEntitiesData entities)
        {
            this.entities = entities;
        }

        public bool Import(string file, ref List<Object> result)
        {
            if (entities == null)
                return false;

            return Import(file, entities, ref result);
        }

        protected abstract bool Import(string file, IEntitiesData entities, ref List<Object> result);
    }
}