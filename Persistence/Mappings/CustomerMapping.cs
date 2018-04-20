using System.Data.Entity.ModelConfiguration;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Mappings
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
        {
            ToTable("Customer");
            HasKey(x => x.Id);

            Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(x => x.RG).HasColumnName("RG").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(x => x.CPF).HasColumnName("CPF").HasColumnType("varchar").HasMaxLength(11).IsRequired();
            Property(x => x.CNPJ).HasColumnName("CNPJ").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(x => x.HomePhone).HasColumnName("HomePhone").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(x => x.CommercialPhone).HasColumnName("CommercialPhone").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(x => x.CellPhone).HasColumnName("CellPhone").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(x => x.CEP).HasColumnName("CEP").HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(x => x.StreetAddress).HasColumnName("StreetAddress").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(x => x.Complement).HasColumnName("Complement").HasColumnType("varchar").HasMaxLength(20).IsRequired();
            Property(x => x.District).HasColumnName("District").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(x => x.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(x => x.State).HasColumnName("State").HasColumnType("varchar").HasMaxLength(2).IsRequired();
            
            Property(x => x.RegisterDate).HasColumnName("RegisterDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");

            Property(x => x.UserId).HasColumnName("UserId").HasColumnType("bigint").IsRequired();
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}