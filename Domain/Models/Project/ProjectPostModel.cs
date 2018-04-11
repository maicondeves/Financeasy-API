using System;
using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Models
{
    public class ProjectPostModel
    {
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

        public long CustomerId { get; set; }
        public long CategoryId { get; set; }
        public long UserId { get; set; }
    }
}