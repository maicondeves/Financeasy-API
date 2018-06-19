using System.Collections.Generic;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Domain.Models
{
    public class DashboardModel
    {
        public decimal TotalExpense { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal Balance { get; set; }

        public DashboardDataModel TotalRevenuePerCategory { get; set; }
        public DashboardDataModel TotalExpensePerCategory { get; set; }
        public DashboardDataModel TotalProjectPerCategory { get; set; }

        public List<Expense> ExpensesCloseToExpiration { get; set; }
    }
}