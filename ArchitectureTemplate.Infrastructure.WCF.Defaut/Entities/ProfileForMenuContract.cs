using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Defaut.Entities
{
    [DataContract]
    public class ProfileForMenuContract
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public int ProfileId { get; set; }

        [DataMember]
        public int MenuId { get; set; }

        [DataMember]
        public virtual ProfileContract Profile { get; set; }

        [DataMember]
        public virtual MenuContract Menu { get; set; }
    }
}
