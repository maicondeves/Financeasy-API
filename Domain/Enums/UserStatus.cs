using System.ComponentModel;

namespace Financeasy.Api.Domain.Enums
{
    public enum UserStatus : short
    {
        [Description("Ativo")]
        Active = 1,
        [Description("Inativo")]
        Inactive = 2,
        [Description("Bloqueado")]
        Blocked = 3,
        [Description("Não confirmado")]
        NotConfirmed = 4
    }
}