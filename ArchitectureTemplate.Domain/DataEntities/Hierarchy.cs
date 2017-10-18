using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class Hierarchy
    {
        public long Id { get; set; }
        public long? HierarchyPaiId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int? HierarchyTypeId { get; set; }
        public bool Vertical { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Hierarchy> HierarchyDown { get; set; }
        public virtual Hierarchy HierarchyUp { get; set; }
        public virtual HierarchyType HierarchyType { get; set; }
        public virtual HierarchyDetail HierarchyDetalhe { get; set; }
        public virtual IEnumerable<User> User { get; set; }
    }
}
