using Levolut.Api.V2.Infrastructure.Database.Models;
using Levolut.Api.V2.QueryHandlers;
using Levolut.Api.V2.Services.Interfaces;

namespace Levolut.Api.V2.Controllers;

public class MoneyExchanger : IMoneyExchanger
{
    private readonly ICurrencyProvider _currencyProvider;
    private IQueryHandler<GetBankFeeRuleQuery, BankFeeRule> _getBankFeeRuleQueryHandler;

    public MoneyExchanger(ICurrencyProvider currencyProvider, IQueryHandler<GetBankFeeRuleQuery, BankFeeRule> getBankFeeRuleQueryHandler)
    {
        _currencyProvider = currencyProvider;
        _getBankFeeRuleQueryHandler = getBankFeeRuleQueryHandler;
    }

    public decimal Exchange(long bankId, MoneyExchange moneyExchange, Currency fromCurrency)
    {
        var bankFeeRule = _getBankFeeRuleQueryHandler.Handle(new GetBankFeeRuleQuery(bankId));
        RequireNotBlockedCountry(bankFeeRule.BlockedCountries, moneyExchange.FromCountry);
        var exchanged = ExchangeMoney(moneyExchange, fromCurrency, bankFeeRule.Fee);

        return exchanged;
    }

    private decimal ExchangeMoney(MoneyExchange moneyExchange, Currency fromCurrency, decimal fee)
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
            throw new InvalidOperationException("Country is blocked.");
        }
    }
}