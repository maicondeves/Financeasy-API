using System;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Models
{
    public class ExpensePutModel
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
        public long CategoryId { get; set; }
    }
}