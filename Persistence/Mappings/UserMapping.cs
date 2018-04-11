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
            HasIndex(u => u.Email).IsUnique();

            Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(x => x.Password).HasColumnName("Password").HasColumnType("varchar").IsRequired();
            Property(x => x.Status).HasColumnName("Status").HasColumnType("smallint").IsRequired();
            Property(x => x.Attempts).HasColumnName("Attempts").HasColumnType("smallint").IsRequired();
            Property(x => x.RegisterDate).HasColumnName("RegisterDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");
        }
    }
}