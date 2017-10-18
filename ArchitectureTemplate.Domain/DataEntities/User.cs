using System;
using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class User
    {
        public long Id { get; set; }
        public long? HierarchyId { get; set; }
        public int ProfileId { get; set; }
        public string Nome { get; set; }
        public long? Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
        public DateTime? DataExpiracaoSenha { get; set; }
        public DateTime? InvalidacaoProgramadaInicio { get; set; }
        public DateTime? InvalidacaoProgramadaFim { get; set; }
        public bool Ativo { get; set; }
        public string CodigoRecover { get; set; }
        public virtual Hierarchy Hierarchy { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual IEnumerable<Log> Log { get; set; }
    }
}

