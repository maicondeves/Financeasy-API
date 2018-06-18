using System.ComponentModel;

namespace Financeasy.Api.Domain.Enums
{
    public enum RevenueStatus : short
    {
        [Description("Em Aberto")]
        Aberto = 1,
        [Description("Recebido")]
        Recebido = 2
    }
}