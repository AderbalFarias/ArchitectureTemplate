using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class TipoHierarquia
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Hierarquia> Hierarquia { get; set; }
    }
}
