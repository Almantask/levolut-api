using Microsoft.AspNetCore.Mvc;

namespace BarberApi.Controllers
{
    internal class GetBalanceResponse
    {
        public decimal Balance { get; internal set; }
    }
}