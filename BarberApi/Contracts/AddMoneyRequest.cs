using LevolutApi.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace BarberApi.Controllers
{
    public class AddMoneyRequest
    {

        public decimal Amount { get; set; }

        [TypeConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }
        public string Country { get; set; }
    }
}