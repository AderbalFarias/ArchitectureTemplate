using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Business.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            HasKey(k => k.Id);

            Property(p => p.Mensagem)
                .IsRequired()
                .HasMaxLength(1000);

            Property(p => p.Conteudo)
                .IsRequired()
                .HasColumnType("varchar(max)")
                .HasMaxLength(1073741824);
            
            HasRequired(p => p.LogType)
                .WithMany()
                .HasForeignKey(f => f.LogTypeId);

            HasOptional(p => p.Tela)
                .WithMany()
                .HasForeignKey(f => f.TelaId);

            HasOptional(p => p.Usuario)
                .WithMany()
                .HasForeignKey(f => f.UsuarioId);
        }
    }
}
