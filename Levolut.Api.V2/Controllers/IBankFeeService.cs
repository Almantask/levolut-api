using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Controllers
{
    public interface IBankFeeService
    {
        BankFeeRule GetBankFeeRule(long bankId);
        BankFeeRule AddBankFeeRule(BankFeeRule bankFeeRule);
    }
}