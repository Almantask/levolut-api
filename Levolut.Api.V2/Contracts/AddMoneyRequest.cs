using Levolut.Api.V2.Infrastructure.Database.Models;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using Levolut.Api.V2.Controllers;

namespace Levolut.Api.V2.Contracts
{
    public record AddMoneyRequest(MoneyExchange MoneyExchange);
}