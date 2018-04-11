using System;
using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Models
{
    public class RevenuePutModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public RevenueStatus Status { get; set; }
        public decimal ReceivableAmount { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public Month MonthPeriod { get; set; }
        public short YearPeriod { get; set; }
        public long CategoryId { get; set; }
    }
}