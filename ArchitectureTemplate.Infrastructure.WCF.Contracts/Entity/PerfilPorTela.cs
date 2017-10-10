using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entity
{
    [DataContract]
    public class PerfilPorTela
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public int PerfilId { get; set; }

        [DataMember]
        public int TelaId { get; set; }

        [DataMember]
        public bool Create { get; set; }

        [DataMember]
        public bool Read { get; set; }

        [DataMember]
        public bool Update { get; set; }

        [DataMember]
        public bool Delete { get; set; }

        [DataMember]
        public virtual Perfil Perfil { get; set; }

        [DataMember]
        public virtual Tela Tela { get; set; }
    }
}
