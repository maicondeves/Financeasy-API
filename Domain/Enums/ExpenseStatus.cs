using System.ComponentModel;

namespace Financeasy.Api.Domain.Enums
{
    public enum ExpenseStatus : short
    {
        [Description("Em Aberto")]
        Aberto = 1,
        [Description("Pago")]
        Pago = 2
    }
}