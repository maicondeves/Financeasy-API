using System.Data.Entity.ModelConfiguration;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Mappings
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
        {
            ToTable("Customer");
            HasKey(c => c.Id);

            Property(c => c.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(c => c.RG).HasColumnName("RG").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(c => c.CPF).HasColumnName("CPF").HasColumnType("varchar").HasMaxLength(11).IsRequired();
            Property(c => c.CNPJ).HasColumnName("CNPJ").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(c => c.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(c => c.HomePhone).HasColumnName("HomePhone").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(c => c.CommercialPhone).HasColumnName("CommercialPhone").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(c => c.CellPhone).HasColumnName("CellPhone").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(c => c.CEP).HasColumnName("CEP").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(c => c.StreetAddress).HasColumnName("StreetAddress").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(c => c.Complement).HasColumnName("Complement").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(c => c.District).HasColumnName("District").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(c => c.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(c => c.State).HasColumnName("State").HasColumnType("varchar").HasMaxLength(2).IsRequired();
            
            Property(c => c.RegisterDate).HasColumnName("RegisterDate").HasColumnType("datetime").IsRequired();
            Property(c => c.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");

            Property(c => c.UserId).HasColumnName("UserId").HasColumnType("bigint").IsRequired();
            HasRequired(c => c.User).WithMany().HasForeignKey(c => c.UserId);
        }
    }
}