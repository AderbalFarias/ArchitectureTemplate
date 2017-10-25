using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Defaut.Entities
{
    [DataContract]
    public class ProfileForScreenContract
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public int ProfileId { get; set; }

        [DataMember]
        public int ScreenId { get; set; }

        [DataMember]
        public bool Create { get; set; }

        [DataMember]
        public bool Read { get; set; }

        [DataMember]
        public bool Update { get; set; }

        [DataMember]
        public bool Delete { get; set; }

        [DataMember]
        public virtual ProfileContract Profile { get; set; }

        [DataMember]
        public virtual ScreenContract Screen { get; set; }
    }
}
