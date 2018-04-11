﻿using System.ComponentModel;

namespace Financeasy.Api.Domain.Enums
{
    public enum Month : short
    {
        [Description("Janeiro")]
        January = 1,
        [Description("Fevereiro")]
        February = 2,
        [Description("Março")]
        March = 3,
        [Description("Abril")]
        April = 4,
        [Description("Maio")]
        May = 5,
        [Description("Junho")]
        June = 6,
        [Description("Julho")]
        July = 7,
        [Description("Agosto")]
        August = 8,
        [Description("Setembro")]
        Setember = 9,
        [Description("Outubro")]
        October = 10,
        [Description("Novembro")]
        November = 11,
        [Description("Dezembro")]
        December = 12,
    }
}
