using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<PerfilPorMenu> PerfilPorMenu { get; set; }
    }
}
