﻿using Levolut.Api.V2.Infrastructure.Database.Models;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace Levolut.Api.V2.Contracts
{
    public class AddMoneyRequest
    {

        public decimal Amount { get; set; }

        [TypeConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }
        public string Country { get; set; }
    }
}