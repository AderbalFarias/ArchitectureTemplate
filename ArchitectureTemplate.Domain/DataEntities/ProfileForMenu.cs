namespace ArchitectureTemplate.Domain.DataEntities
{
    public class ProfileForMenu
    {
        public long Id { get; set; }
        public int ProfileId { get; set; }
        public int MenuId { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
