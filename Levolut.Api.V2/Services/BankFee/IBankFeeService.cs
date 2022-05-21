using Levolut.Api.V2.Database.Models;

namespace Levolut.Api.V2.Services.BankFee
{
    public interface IBankFeeService
    {
        BankFeeRule GetBankFeeRule(long bankId);
        BankFeeRule AddBankFeeRule(BankFeeRule bankFeeRule);
    }
}