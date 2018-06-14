using System;
using Financeasy.Api.Domain.Enums;
using Newtonsoft.Json;

namespace Financeasy.Api.Domain.Entities
{
    public class Expense
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public ExpenseStatus Status { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Month MonthPeriod { get; set; }
        public short YearPeriod { get; set; }

        [JsonIgnore]
        public DateTime RegisterDate { get; set; }
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; }

        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}