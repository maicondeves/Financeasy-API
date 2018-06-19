using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Enums;
using Financeasy.Api.Domain.Models;
using System;
using System.Linq;

namespace Financeasy.Api.Applications
{
    public class DashboardApplication
    {
        [Inject]
        private RevenueApplication _revenueApplication { get; set; }

        [Inject]
        private ExpenseApplication _expenseApplication { get; set; }

        [Inject]
        private ProjectApplication _projectApplication { get; set; }

        public DashboardModel GetDashboard(long userId)
        {
            var totalExpense = _expenseApplication.GetAll(userId).Where(x => x.MonthPeriod == (Month) DateTime.Now.Month && x.YearPeriod == DateTime.Now.Year).Sum(x => x.Amount);
            var totalRevenue = _revenueApplication.GetAll(userId).Where(x => x.MonthPeriod == (Month) DateTime.Now.Month && x.YearPeriod == DateTime.Now.Year).Sum(x => x.ReceivableAmount);
            var balance = totalRevenue - totalExpense;

            var totalProjectPerCategory = new DashboardDataModel();
            var totalRevenuePerCategory = new DashboardDataModel();
            var totalExpensePerCategory = new DashboardDataModel();

            var projectsPerCategory = _projectApplication.GetProjectsPerCategory(userId);
            if (projectsPerCategory.Count > 0)
            {
                foreach (var item in projectsPerCategory)
                {
                    var teste = item;
                    totalProjectPerCategory.Labels.Add(item.CategoryName);
                    totalProjectPerCategory.Values.Add(item.ProjectAmount);
                }
            }
            

            var revenuesPerCategory = _revenueApplication.GetRevenuesPerCategory(userId);
            if (revenuesPerCategory.Count > 0)
            {
                foreach (var item in revenuesPerCategory)
                {
                    totalRevenuePerCategory.Labels.Add(item.CategoryName);
                    totalRevenuePerCategory.Values.Add(item.TotalAmount);
                }
            }
            
            var expensesPerCategory = _expenseApplication.GetExpensesPerCategory(userId);
            if (expensesPerCategory.Count > 0)
            {
                foreach (var item in expensesPerCategory)
                {
                    totalExpensePerCategory.Labels.Add(item.CategoryName);
                    totalExpensePerCategory.Values.Add(item.TotalAmount);
                }
            }

            var expensesCloseToExpiration = _expenseApplication.GetExpensesCloseToExpiration(userId);

            return new DashboardModel()
            {
                TotalExpense = totalExpense,
                TotalRevenue = totalRevenue,
                Balance = balance,
                TotalExpensePerCategory = totalExpensePerCategory,
                TotalRevenuePerCategory = totalRevenuePerCategory,
                TotalProjectPerCategory = totalProjectPerCategory,
                ExpensesCloseToExpiration = expensesCloseToExpiration,
            };
        }
    }
}