using System;
using System.Runtime.Serialization;

namespace ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities
{
    [DataContract]
    public class UsuarioContract
    {
        [DataMember]
        public long Id { get; set; }

        //public long? HierarquiaId { get; set; }

        [DataMember]
        public int PerfilId { get; set; }

        [DataMember]
        public string Nome { get; set; }

        public long? Cpf { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Telefone { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Token { get; set; }

        public DateTime? DataExpiracaoSenha { get; set; }

        public DateTime? InvalidacaoProgramadaInicio { get; set; }

        public DateTime? InvalidacaoProgramadaFim { get; set; }

        [DataMember]
        public bool Ativo { get; set; }

        public string CodigoRecover { get; set; }

        //public virtual Hierarquia Hierarquia { get; set; }

        public virtual PerfilContract Perfil { get; set; }

        //public virtual IEnumerable<Log> Log { get; set; }
    }
}

