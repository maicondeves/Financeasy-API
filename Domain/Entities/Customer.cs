using System;
using Newtonsoft.Json;

namespace Financeasy.Api.Domain.Entities
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }

        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string CommercialPhone { get; set; }
        public string CellPhone { get; set; }

        public string CEP { get; set; }
        public string StreetAddress { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [JsonIgnore]
        public DateTime? RegisterDate { get; set; }
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; }

        public long UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}