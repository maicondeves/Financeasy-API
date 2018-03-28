using System;

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

        public DateTime? RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}