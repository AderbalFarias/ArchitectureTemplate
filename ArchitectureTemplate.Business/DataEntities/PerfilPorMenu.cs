namespace ArchitectureTemplate.Business.DataEntities
{
    public class PerfilPorMenu
    {
        public long Id { get; set; }
        public int PerfilId { get; set; }
        public int  MenuId { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
