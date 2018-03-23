using System.Data.Entity;
using Financeasy.Api.Persistence.Mappings;

namespace Financeasy.Api.Persistence.Configs
{
    public class FinanceasyConfig : FinanceasyContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}