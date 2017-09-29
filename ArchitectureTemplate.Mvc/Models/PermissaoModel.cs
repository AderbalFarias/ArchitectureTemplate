using System.Collections.Generic;
using System.ComponentModel;
using ArchitectureTemplate.Business.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class PermissaoModel
    {
        [DisplayName(@"Perfil")]
        public int PerfilId { get; set; }

        public IEnumerable<Tela> TelaList { get; set; }

        public IEnumerable<PerfilPorTela> PerfilPorTelaList { get; set; }

        public IDictionary<int, string> PerfilDictionary { get; set; }

        public double Scroll { get; set; }
    }
}