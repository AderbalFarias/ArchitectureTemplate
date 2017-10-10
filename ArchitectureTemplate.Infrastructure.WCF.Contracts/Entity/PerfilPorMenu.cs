using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entity
{
    [DataContract]
    public class PerfilPorMenu
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public int PerfilId { get; set; }

        [DataMember]
        public int MenuId { get; set; }

        [DataMember]
        public virtual Perfil Perfil { get; set; }

        [DataMember]
        public virtual Menu Menu { get; set; }
    }
}
