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
        public virtual IEnumerable<User> User { get; set; }
        public virtual ICollection<ProfileForScreen> ProfilePorScreen { get; set; }
        public virtual IEnumerable<ProfileForMenu> ProfilePorMenu { get; set; }
    }
}


