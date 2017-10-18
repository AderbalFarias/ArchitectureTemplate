using System.Collections.Generic;
using System.ComponentModel;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class PermissaoModel
    {
        [DisplayName(@"Profile")]
        public int ProfileId { get; set; }

        public IEnumerable<Tela> TelaList { get; set; }

        public IEnumerable<ProfilePorTela> ProfilePorTelaList { get; set; }

        public IDictionary<int, string> ProfileDictionary { get; set; }

        public double Scroll { get; set; }
    }
}