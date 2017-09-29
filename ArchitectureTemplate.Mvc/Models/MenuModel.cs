using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ArchitectureTemplate.Business.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class MenuModel
    {
        [DisplayName(@"Identificador")]
        public int Id { get; set; }

        [DisplayName(@"Nome")]
        [Required(ErrorMessage = @"Campo Obrigatório")]
        [StringLength(50, ErrorMessage = @"O campo deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [DisplayName(@"Perfil")]
        public int PerfilId { get; set; }

        public IEnumerable<Menu> MenuList { get; set; }

        public IEnumerable<PerfilPorMenu> PerfilPorMenuList { get; set; }

        public IDictionary<int, string> PerfilDictionary { get; set; }

        public double Scroll { get; set; }
    }
}
