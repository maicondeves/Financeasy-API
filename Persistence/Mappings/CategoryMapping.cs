using System.Data.Entity.ModelConfiguration;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Mappings
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            ToTable("Category");
            HasKey(c => c.Id);

            Property(c => c.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(c => c.Type).HasColumnName("Type").HasColumnType("smallint").IsRequired();

            Property(c => c.RegisterDate).HasColumnName("RegisterDate").HasColumnType("datetime").IsRequired();
            Property(c => c.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");

            Property(c => c.UserId).HasColumnName("UserId").HasColumnType("bigint").IsRequired();
            HasRequired(c => c.User).WithMany().HasForeignKey(c => c.UserId);
        }
    }
}