using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities
{
    [DataContract]
    public class PerfilContract
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
        public virtual IEnumerable<UsuarioContract> Usuario { get; set; }

        [DataMember]
        public virtual ICollection<PerfilPorTelaContract> PerfilPorTela { get; set; }

        [DataMember]
        public virtual IEnumerable<PerfilPorMenuContract> PerfilPorMenu { get; set; }
    }
}


