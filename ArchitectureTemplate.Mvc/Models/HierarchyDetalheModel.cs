using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class HierarchyDetalheModel
    {
        public long HierarchyId { get; set; }

        [DisplayName(@"Pessoa Física")]
        public bool PessoaFisica { get; set; }

        public string PessoaContato { get; set; }

        [DataType(DataType.Text)]
        [DisplayName(@"Cpf / Cnpj")]
        public long? CpfCnpj { get; set; }
        public string Telefone { get; set; }

        [StringLength(100, ErrorMessage = @"Field must be 100 characters or less")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = @"Endereço de e-mail inválido")]
        public string Email { get; set; }

        [DisplayName(@"Code")]
        public int? Codigo { get; set; }
        public virtual Hierarchy Hierarchy { get; set; }
    }
}
