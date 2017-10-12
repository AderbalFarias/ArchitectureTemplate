using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class Hierarquia
    {
        public long Id { get; set; }
        public long? HierarquiaPaiId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int? TipoHierarquiaId { get; set; }
        public bool Vertical { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Hierarquia> HierarquiaDown { get; set; }
        public virtual Hierarquia HierarquiaUp { get; set; }
        public virtual TipoHierarquia TipoHierarquia { get; set; }
        public virtual HierarquiaDetalhe HierarquiaDetalhe { get; set; }
        public virtual IEnumerable<Usuario> Usuario { get; set; }
    }
}
