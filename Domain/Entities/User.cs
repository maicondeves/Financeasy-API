using System;
using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
        public short Attempts { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }


}