using System.Collections.Generic;

namespace Financeasy.Api.Domain.Models
{
    public class DashboardDataModel
    {
        public List<string> Labels { get; set; }
        public List<decimal> Values { get; set; }

        public DashboardDataModel()
        {
            Labels = new List<string>();
            Values = new List<decimal>();
        }
    }
}