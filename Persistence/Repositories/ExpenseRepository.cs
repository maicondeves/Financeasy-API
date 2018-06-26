using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Models;

namespace Financeasy.Api.Persistence.Repositories
{
    [Register]
    public class ExpenseRepository : Repository<Expense>, IRepository<Expense>
    {
        public List<ExpenseCategoryModel> GetExpensesPerCategory(long userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                return conn.Query<ExpenseCategoryModel>("SELECT T1.Name AS CategoryName, Sum(T2.Amount) AS TotalAmount FROM Category T1 INNER JOIN Expense T2 ON (T2.CategoryId = T1.Id) WHERE T1.UserId = @UserId AND T2.UserId = @UserId AND T2.MonthPeriod = @Month AND T2.YearPeriod = @Year GROUP BY T1.Name;", new { UserId = userId, Month = DateTime.Now.Month, Year = DateTime.Now.Year }).ToList();
        }

        public decimal GetTotalExpenses(long userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                return conn.Query<decimal>("SELECT IsNull(Sum(Amount), 0) AS decimal FROM Expense WHERE UserId = @UserId AND MonthPeriod = @Month AND YearPeriod = @Year;", new { UserId = userId, Month = DateTime.Now.Month, Year = DateTime.Now.Year }).FirstOrDefault();
        }
    }
}