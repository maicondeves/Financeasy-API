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
    public class RevenueRepository : Repository<Revenue>, IRepository<Revenue>
    {
        public List<RevenueCategoryModel> GetRevenuesPerCategory(long userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                return conn.Query<RevenueCategoryModel>("SELECT T1.Name AS CategoryName, Sum(T2.ReceivableAmount) AS TotalAmount FROM Category T1 INNER JOIN Revenue T2 ON (T2.CategoryId = T1.Id) WHERE T1.UserId = @UserId AND T2.UserId = @UserId AND T2.MonthPeriod = @Month AND T2.YearPeriod = @Year GROUP BY T1.Name;", new { UserId = userId, Month = DateTime.Now.Month, Year = DateTime.Now.Year }).ToList();
        }

        public decimal GetTotalRevenues(long userId)
        {
            decimal teste = 0;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                teste = conn.Query<decimal>("SELECT IsNull(Sum(ReceivableAmount), 0) AS decimal FROM Revenue WHERE UserId = @UserId AND MonthPeriod = @Month AND YearPeriod = @Year;", new { UserId = userId, Month = DateTime.Now.Month, Year = DateTime.Now.Year }).FirstOrDefault();

            return teste;
        }
    }
}