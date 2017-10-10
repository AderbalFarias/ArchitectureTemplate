using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entity
{
    [DataContract]
    public class Tela
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string ControllerName { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public bool Create { get; set; }

        [DataMember]
        public bool Read { get; set; }

        [DataMember]
        public bool Update { get; set; }

        [DataMember]
        public bool Delete { get; set; }

        [DataMember]
        public ICollection<PerfilPorTela> PerfilPorTela { get; set; }
    }
}