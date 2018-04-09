using System;

namespace Financeasy.Api.Domain.Models
{
    public class UserViewProfileModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}