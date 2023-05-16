using Levolut.Api.V3.Infrastructure.Database.Entities;

namespace Levolut.Api.V3.Domain.Models;

// Introducing a breaking change.
public record MoneyExchange(decimal Amount, Currency Currency, string FromCountry);