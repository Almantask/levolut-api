﻿using Levolut.Api.V2.Database.Models;

namespace Levolut.Api.V2.Services
{
    public interface ICurrencyProvider
    {
        decimal GetRate(Currency currency);
    }
}