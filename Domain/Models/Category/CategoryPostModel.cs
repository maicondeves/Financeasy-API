using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Models
{
    public class CategoryPostModel
    {
        public string Name { get; set; }
        public CategoryType Type { get; set; }

        public long UserId { get; set; }
    }
}