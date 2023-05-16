using Levolut.Api.V3.Infrastructure.Database.Entities;

namespace Levolut.Api.V3.Domain.Services.BankFee
{
    public interface IBankFeeService
    {
        BankFeeRule GetBankFeeRule(long bankId);
        BankFeeRule AddBankFeeRule(BankFeeRule bankFeeRule);
    }
}