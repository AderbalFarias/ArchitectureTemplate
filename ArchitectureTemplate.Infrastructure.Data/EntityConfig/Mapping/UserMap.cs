using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(k => k.Id);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(80);

            Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UQ_User_Email", 1) { IsUnique = true }));

            Property(p => p.Telefone)
                .HasMaxLength(15);

            Property(p => p.Login)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UQ_User_Login", 2) { IsUnique = true }));

            Property(p => p.Senha)
                .HasMaxLength(500);

            Property(p => p.CodigoRecover)
                .HasMaxLength(20);

            HasOptional(p => p.Hierarchy)
                .WithMany()//.WithMany(p => p.User)
                .HasForeignKey(f => f.HierarchyId);

            HasRequired(p => p.Profile)
                .WithMany()
                .HasForeignKey(f => f.ProfileId);
        }
    }
}
