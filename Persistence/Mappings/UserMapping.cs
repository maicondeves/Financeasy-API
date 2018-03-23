using System.Data.Entity.ModelConfiguration;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("User");
            HasKey(u => u.Id);

            Property(c => c.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(c => c.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(c => c.Password).HasColumnName("Password").HasColumnType("varchar").HasMaxLength(20).IsRequired();
            Property(c => c.Status).HasColumnName("Status").HasColumnType("smallint").IsRequired();
            Property(c => c.Attempts).HasColumnName("Attempts").HasColumnType("smallint").IsRequired();
            Property(c => c.RegisterDate).HasColumnName("RegisterDate").HasColumnType("datetime").IsRequired();
            Property(c => c.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");
        }
    }
}