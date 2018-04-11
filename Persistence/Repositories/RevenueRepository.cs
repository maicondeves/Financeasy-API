﻿using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;

namespace Financeasy.Api.Persistence.Repositories
{
    [Register]
    public class RevenueRepository : Repository<Revenue>, IRepository<Revenue>
    {
    }
}