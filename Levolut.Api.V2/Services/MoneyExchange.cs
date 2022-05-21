using Levolut.Api.V2.Database.Models;

namespace Levolut.Api.V2.Services;

public record MoneyExchange(decimal Amount, Currency Currency, string FromCountry);