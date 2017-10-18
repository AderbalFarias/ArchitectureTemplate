using System;
using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Solicitante { get; set; }
        public DateTime DataCadastro { get; set; }
        public virtual IEnumerable<Usuario> Usuario { get; set; }
        public virtual ICollection<ProfilePorTela> ProfilePorTela { get; set; }
        public virtual IEnumerable<ProfilePorMenu> ProfilePorMenu { get; set; }
    }
}


