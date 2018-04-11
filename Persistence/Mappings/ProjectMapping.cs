using System.Data.Entity.ModelConfiguration;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Mappings
{
    public class ProjectMapping : EntityTypeConfiguration<Project>
    {
        public ProjectMapping()
        {
            ToTable("Project");
            HasKey(x => x.Id);

            Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(x => x.Description).HasColumnName("Description").HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(x => x.Status).HasColumnName("Status").HasColumnType("smallint").IsRequired();
            Property(x => x.StartDate).HasColumnName("StartDate").HasColumnType("date").IsOptional();
            Property(x => x.ConclusionDate).HasColumnName("ConclusionDate").HasColumnType("date").IsOptional();
            
            Property(x => x.CEP).HasColumnName("CEP").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(x => x.StreetAddress).HasColumnName("StreetAddress").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(x => x.Complement).HasColumnName("Complement").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(x => x.District).HasColumnName("District").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(x => x.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(x => x.State).HasColumnName("State").HasColumnType("varchar").HasMaxLength(2).IsRequired();

            Property(x => x.RegisterDate).HasColumnName("RegisterDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");

            Property(x => x.CustomerId).HasColumnName("CustomerId").HasColumnType("bigint").IsRequired();
            HasRequired(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId);

            Property(x => x.CategoryId).HasColumnName("CategoryId").HasColumnType("bigint").IsRequired();
            HasRequired(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);

            Property(x => x.UserId).HasColumnName("UserId").HasColumnType("bigint").IsRequired();
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId);

            HasMany(x => x.Revenues).WithRequired().HasForeignKey(x => x.Id);
            HasMany(x => x.Expenses).WithRequired().HasForeignKey(x => x.Id);
        }
    }
}