using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArchitectureTemplate.Mvc.Models
{
    public class PerfilModel
    {
        [Key]
        [DisplayName(@"Código")]
        public int Id { get; set; }

        [DisplayName(@"Perfil")]
        [Required(ErrorMessage = @"Campo Obrigatório")]
        [StringLength(50, ErrorMessage = @"O campo deve ter no máximo 50 caracters")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string Solicitante { get; set; }

        [DisplayName(@"Data de Cadastro")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        //public IEnumerable<Usuario> Usuario { get; set; }

        public IDictionary<int, string> AbrangenciaDictionary { get; set; }

        public IDictionary<int, string> AreaDictionary { get; set; }
    }
}
