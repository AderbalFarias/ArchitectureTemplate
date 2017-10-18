namespace ArchitectureTemplate.Domain.DataEntities
{
    public class HierarchyDetail
    {
        public long HierarchyId { get; set; }
        public string PessoaContato { get; set; }
        public bool PessoaFisica { get; set; }
        public long? CpfCnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int? Codigo { get; set; }
        public virtual Hierarchy Hierarchy { get; set; }
    }
}
