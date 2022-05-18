using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Contracts
{
    public class CashMoneyRequest
    {
        public string Country { get; set; }

        public Currency Currency { get; set; }

        public decimal Amount { get; set; }
    }
}