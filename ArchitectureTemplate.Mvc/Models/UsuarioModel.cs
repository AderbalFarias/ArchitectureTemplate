using ArchitectureTemplate.Business.DataEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArchitectureTemplate.Mvc.Models
{
    public class UsuarioModel
    {
        public long Id { get; set; }

        [DisplayName(@"Hierarquia")]
        public long? HierarquiaId { get; set; }

        [Required(ErrorMessage = @"Campo Obrigatório")]
        [DisplayName(@"Perfil")]
        public int PerfilId { get; set; }

        [DisplayName(@"Código do Agente")]
        public int? CodigoAgente { get; set; }

        [DisplayName(@"Ramal do Agente")]
        public int? RamalAgente { get; set; }

        [Required(ErrorMessage = @"Campo Obrigatório")]
        public string Nome { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = @"Campo Obrigatório")]
        [DisplayFormat(DataFormatString = "{0:000\\.000\\.000-00}")]
        [DisplayName(@"Cpf")]
        public long? Cpf { get; set; }

        [Required(ErrorMessage = @"Campo Obrigatório")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = @"Endereço de e-mail inválido")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(00)000000000}")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = @"Campo Obrigatório")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public string Token { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName(@"Data de Expiração da Senha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataExpiracaoSenha { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName(@"Data Inicial de Invalidação")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? InvalidacaoProgramadaInicio { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName(@"Data Final de Invalidação")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? InvalidacaoProgramadaFim { get; set; }

        public bool Ativo { get; set; }

        public string CodigoRecover { get; set; }

        public Hierarquia Hierarquia { get; set; }

        public Perfil Perfil { get; set; }

        public IDictionary<int, string> PerfilDictionary { get; set; }

        public IDictionary<long, string> HierarquiaDictionary { get; set; }

        public IDictionary<int, string> CentroCustoDictionary { get; set; }
    }
}
