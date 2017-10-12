using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.DataEntities
{
    public class PerfilPorTela
    {
        public long Id { get; set; }
        public int PerfilId { get; set; }
        public int TelaId { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Tela Tela { get; set; }

        public string DisplayFuncionalidades(PerfilPorTela entity)
        {
            IList<string> list = new List<string>();

            if (entity.Read)
                list.Add("Consultar");
            if (entity.Create)
                list.Add("Cadastrar");
            if (entity.Update)
                list.Add("Atualizar");
            if (entity.Delete)
                list.Add("Deletar");

            return string.Join("; ", list);
        }

        public string DisplayBool(bool value)
        {
            return value ? "Sim" : "Não";
        }
    }
}
