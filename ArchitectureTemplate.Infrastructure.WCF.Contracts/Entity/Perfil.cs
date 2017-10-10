using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entity
{
    [DataContract]
    public class Perfil
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public bool Ativo { get; set; }

        [DataMember]
        public string Solicitante { get; set; }

        [DataMember]
        public DateTime DataCadastro { get; set; }

        [DataMember]
        public virtual IEnumerable<Usuario> Usuario { get; set; }

        [DataMember]
        public virtual ICollection<PerfilPorTela> PerfilPorTela { get; set; }

        [DataMember]
        public virtual IEnumerable<PerfilPorMenu> PerfilPorMenu { get; set; }
    }
}


