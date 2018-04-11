using System.Data.Entity.ModelConfiguration;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Mappings
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            ToTable("Category");
            HasKey(x => x.Id);

            Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(x => x.Type).HasColumnName("Type").HasColumnType("smallint").IsRequired();

            Property(x => x.RegisterDate).HasColumnName("RegisterDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");

            Property(x => x.UserId).HasColumnName("UserId").HasColumnType("bigint").IsRequired();
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}