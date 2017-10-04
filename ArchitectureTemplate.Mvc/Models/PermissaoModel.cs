using ArchitectureTemplate.Business.DataEntities;
using System.Collections.Generic;
using System.ComponentModel;

namespace ArchitectureTemplate.Mvc.Models
{
    public class PermissaoModel
    {
        [DisplayName(@"Role")]
        public int PerfilId { get; set; }

        public IEnumerable<Tela> TelaList { get; set; }

        public IEnumerable<PerfilPorTela> PerfilPorTelaList { get; set; }

        public IDictionary<int, string> PerfilDictionary { get; set; }

        public double Scroll { get; set; }
    }
}