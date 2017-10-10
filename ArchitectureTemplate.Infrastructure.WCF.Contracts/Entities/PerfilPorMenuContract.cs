using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities
{
    [DataContract]
    public class PerfilPorMenuContract
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public int PerfilId { get; set; }

        [DataMember]
        public int MenuId { get; set; }

        [DataMember]
        public virtual PerfilContract Perfil { get; set; }

        [DataMember]
        public virtual MenuContract Menu { get; set; }
    }
}
