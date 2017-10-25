using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Defaut.Entities
{
    [DataContract]
    public class MenuContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public virtual ICollection<ProfileForMenuContract> ProfileForMenu { get; set; }
    }
}
