using ArchitectureTemplate.Business.DataEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            HasKey(k => k.Id);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(80);

            Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UQ_Usuario_Email", 1) { IsUnique = true }));

            Property(p => p.Telefone)
                .HasMaxLength(15);

            Property(p => p.Login)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UQ_Usuario_Login", 2) { IsUnique = true }));

            Property(p => p.Senha)
                .HasMaxLength(500);

            Property(p => p.CodigoRecover)
                .HasMaxLength(20);

            HasOptional(p => p.Hierarquia)
                .WithMany()//.WithMany(p => p.Usuario)
                .HasForeignKey(f => f.HierarquiaId);

            HasRequired(p => p.Perfil)
                .WithMany()
                .HasForeignKey(f => f.PerfilId);
        }
    }
}
