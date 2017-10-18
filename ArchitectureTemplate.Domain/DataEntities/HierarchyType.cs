using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class HierarchyType
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Hierarchy> Hierarchy { get; set; }
    }
}
