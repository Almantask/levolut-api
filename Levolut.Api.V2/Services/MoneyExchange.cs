using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Controllers;

public record MoneyExchange(decimal Amount, Currency Currency, string FromCountry);