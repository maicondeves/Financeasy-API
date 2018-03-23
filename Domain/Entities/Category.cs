using System;
using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}