using LevolutApi.Infrastructure.Database;

namespace BarberApi.Controllers
{
    public class CashMoneyRequest
    {
        public string Country { get; set; }

        public Currency Currency { get; set; }

        public decimal Amount { get; set; }
    }
}