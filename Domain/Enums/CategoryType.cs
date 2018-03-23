using System.ComponentModel;

namespace Financeasy.Api.Domain.Enums
{
    public enum CategoryType : short
    {
        [Description("Projeto")]
        Project = 1,
        [Description("Receita")]
        Revenue = 2,
        [Description("Despesa")]
        Expense = 3
    }
}