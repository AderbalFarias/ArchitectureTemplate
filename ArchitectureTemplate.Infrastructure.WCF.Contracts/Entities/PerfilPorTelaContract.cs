using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities
{
    [DataContract]
    public class PerfilPorTelaContract
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
        public virtual PerfilContract Perfil { get; set; }

        [DataMember]
        public virtual TelaContract Tela { get; set; }
    }
}
