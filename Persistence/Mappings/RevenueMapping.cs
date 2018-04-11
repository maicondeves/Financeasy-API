using System.Data.Entity.ModelConfiguration;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Mappings
{
    public class RevenueMapping : EntityTypeConfiguration<Revenue>
    {
        public RevenueMapping()
        {
            ToTable("Revenue");
            HasKey(x => x.Id);

            Property(x => x.Description).HasColumnName("Description").HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(x => x.Status).HasColumnName("Status").HasColumnType("smallint").IsRequired();
            Property(x => x.ReceivableAmount).HasColumnName("ReceivableAmount").HasColumnType("decimal").IsRequired();
            Property(x => x.ReceivedAmount).HasColumnName("ReceivedAmount").HasColumnType("decimal");
            Property(x => x.ReceivedDate).HasColumnName("ReceivedDate").HasColumnType("date");
            Property(x => x.MonthPeriod).HasColumnName("MonthPeriod").HasColumnType("smallint").IsRequired();
            Property(x => x.YearPeriod).HasColumnName("YearPeriod").HasColumnType("smallint").IsRequired();

            Property(x => x.RegisterDate).HasColumnName("RegisterDate").HasColumnType("datetime").IsRequired();
            Property(x => x.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime");

            Property(x => x.ProjectId).HasColumnName("ProjectId").HasColumnType("bigint").IsRequired();
            HasRequired(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);

            Property(x => x.CategoryId).HasColumnName("CategoryId").HasColumnType("bigint").IsRequired();
            HasRequired(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);

            Property(x => x.UserId).HasColumnName("UserId").HasColumnType("bigint").IsRequired();
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}