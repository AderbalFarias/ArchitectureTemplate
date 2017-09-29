using ArchitectureTemplate.Business.DataEntities;
using System;
using System.ComponentModel;

namespace ArchitectureTemplate.Mvc.Models
{
    public class LogModel
    {
        [DisplayName(@"Id do Log")]
        public long Id { get; set; }

        [DisplayName(@"Tipo de Log")]
        public int LogTypeId { get; set; }

        [DisplayName(@"Tela")]
        public int? TelaId { get; set; }
        
        public long? UsuarioId { get; set; }
        
        public string Mensagem { get; set; }

        [DisplayName(@"Entidade")]
        public string NomeClasse { get; set; }

        [DisplayName(@"Conteúdo")]
        public string Conteudo { get; set; }

        [DisplayName(@"Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        //public Tela Tela { get; set; }

        public LogType LogType { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}