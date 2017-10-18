using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities
{
    [DataContract]
    public class MenuContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public virtual ICollection<ProfilePorMenuContract> ProfilePorMenu { get; set; }
    }
}
