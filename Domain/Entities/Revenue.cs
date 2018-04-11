using System;
using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Entities
{
    public class Revenue
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public RevenueStatus Status { get; set; }
        public decimal ReceivableAmount { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public Month MonthPeriod { get; set; }
        public short YearPeriod { get; set; }

        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}