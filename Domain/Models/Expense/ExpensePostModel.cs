using System;
using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Models
{
    public class ExpensePostModel
    {
        public string Description { get; set; }
        public ExpenseStatus Status { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Month MonthPeriod { get; set; }
        public short YearPeriod { get; set; }

        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public long ProjectId { get; set; }
        public long CategoryId { get; set; }
        public long UserId { get; set; }
    }
}