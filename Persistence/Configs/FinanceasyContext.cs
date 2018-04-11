using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Configs
{
    [Register]
    public class FinanceasyContext : DbContext
    {
        public FinanceasyContext() : base("Default") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}