using Levolut.Api.V2.Database.Entities;

namespace Levolut.Api.V2.Models;

public record MoneyExchange(decimal Amount, Currency Currency, string FromCountry);