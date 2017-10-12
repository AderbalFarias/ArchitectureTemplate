using System;
using System.ComponentModel;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Mvc.Models
{
    public class LogModel
    {
        [DisplayName(@"Id of Log")]
        public long Id { get; set; }

        [DisplayName(@"King of Log")]
        public int LogTypeId { get; set; }

        [DisplayName(@"Screen")]
        public int? TelaId { get; set; }

        public long? UsuarioId { get; set; }

        public string Mensagem { get; set; }

        [DisplayName(@"Entity")]
        public string NomeClasse { get; set; }

        [DisplayName(@"Content")]
        public string Conteudo { get; set; }

        [DisplayName(@"Date of Create")]
        public DateTime DataCadastro { get; set; }

        //public Tela Tela { get; set; }

        public LogType LogType { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}