using Levolut.Api.V2.Database.Entities;

namespace Levolut.Api.V2.Models;

// Introducing a breaking change.
public record MoneyExchange(decimal Amount, Currency Currency, string FromCountry);