using Levolut.Api.V3.Infrastructure.Database.Entities;

namespace Levolut.Api.V3.Domain.Models.ValueObjects;

// Value object - no id, primitive.
// Will not be persisted by itself.
public record MoneyExchange(decimal Amount, Currency Currency, string FromCountry);