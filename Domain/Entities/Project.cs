using System;
using System.Collections.Generic;
using Financeasy.Api.Domain.Enums;
using Newtonsoft.Json;

namespace Financeasy.Api.Domain.Entities
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ConclusionDate { get; set; }

        public string CEP { get; set; }
        public string StreetAddress { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [JsonIgnore]
        public DateTime RegisterDate { get; set; }
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Revenue> Revenues { get; set; }
        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; }

        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}