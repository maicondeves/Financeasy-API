using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Filters
{
    public class ExpenseFilter
    {
        public long ProjectId { get; set; }
        public Month MonthWork { get; set; }
        public int YearWork { get; set; }
    }
}