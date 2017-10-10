using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entity
{
    [DataContract]
    public class Menu
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public virtual ICollection<PerfilPorMenu> PerfilPorMenu { get; set; }
    }
}
