using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Domain.Models
{
    public class CategoryPutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
    }
}