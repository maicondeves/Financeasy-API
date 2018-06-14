using System;
using Financeasy.Api.Domain.Enums;
using Newtonsoft.Json;

namespace Financeasy.Api.Domain.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }

        [JsonIgnore]
        public DateTime? RegisterDate { get; set; }
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}