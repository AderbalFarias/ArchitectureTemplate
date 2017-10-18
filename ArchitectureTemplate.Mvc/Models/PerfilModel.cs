using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArchitectureTemplate.Mvc.Models
{
    public class ProfileModel
    {
        [Key]
        [DisplayName(@"Code")]
        public int Id { get; set; }

        [DisplayName(@"Profile")]
        [Required(ErrorMessage = @"Field Required")]
        [StringLength(50, ErrorMessage = @"Field must be 50 characters or less")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string Solicitante { get; set; }

        [DisplayName(@"Date of Create")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        //public IEnumerable<Usuario> Usuario { get; set; }

        public IDictionary<int, string> AbrangenciaDictionary { get; set; }

        public IDictionary<int, string> AreaDictionary { get; set; }
    }
}
