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
    public class ProjectRepository : Repository<Project>, IRepository<Project>
    {
        public List<ProjectCategoryModel> GetProjectPerCategory(long userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                return conn.Query<ProjectCategoryModel>("SELECT T1.Name AS CategoryName, Count(T2.Id) AS ProjectAmount FROM Category T1 INNER JOIN Project T2 ON (T2.CategoryId = T1.Id) WHERE T1.UserId = @UserId AND T2.UserId = @UserId GROUP BY T1.Name;", new { UserId = userId}).ToList();
        }
    }
}