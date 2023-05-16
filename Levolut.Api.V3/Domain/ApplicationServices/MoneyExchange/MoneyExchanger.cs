using Levolut.Api.V3.Domain.Exceptions;
using Levolut.Api.V3.Domain.Models;
using Levolut.Api.V3.Domain.Models.Entities;
using Levolut.Api.V3.Domain.Query;

namespace Levolut.Api.V3.Domain.Services.MoneyExchange;

public class MoneyExchanger : IMoneyExchanger
{
    private readonly ICurrencyProvider _currencyProvider;
    private readonly IQueryHandler<GetBankFeeRuleQuery, BankFeeRule> _getBankFeeRuleQueryHandler;

    public MoneyExchanger(ICurrencyProvider currencyProvider, IQueryHandler<GetBankFeeRuleQuery, BankFeeRule> getBankFeeRuleQueryHandler)
    {
        _currencyProvider = currencyProvider;
        _getBankFeeRuleQueryHandler = getBankFeeRuleQueryHandler;
    }

    public decimal Exchange(long bankId, Models.ValueObjects.MoneyExchange moneyExchange, Currency fromCurrency)
    {
        var bankFeeRule = _getBankFeeRuleQueryHandler.Handle(new GetBankFeeRuleQuery(bankId));
        RequireNotBlockedCountry(bankFeeRule.BlockedCountries, moneyExchange.FromCountry);
        var exchanged = ExchangeMoney(moneyExchange, fromCurrency, bankFeeRule.Fee);

        return exchanged;
    }

    private decimal ExchangeMoney(Models.ValueObjects.MoneyExchange moneyExchange, Currency fromCurrency, decimal fee)
    {
        var exchangeFee = moneyExchange.Currency == fromCurrency ? 0 : fee;
        var requestCurrencyRate = _currencyProvider.GetRate(moneyExchange.Currency);
        var currentBalanceCurrencyRate = _currencyProvider.GetRate(fromCurrency);
        var exchanged = moneyExchange.Amount / currentBalanceCurrencyRate * requestCurrencyRate * (1 - exchangeFee);

        return exchanged;
    }

    private void RequireNotBlockedCountry(IEnumerable<BlockedCountry> blockedCountries, string country)
    {
        if (blockedCountries.Any(bc => bc.Name == country))
        {
            throw new BlockedCountryException(country);
        }
    }
}