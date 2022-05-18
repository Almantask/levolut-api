using LevolutApi.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace BarberApi.Controllers
{
    internal class GetBankFeeRulesResponse
    {
        public BankFeeRule BankFeeRule { get; internal set; }
    }
}