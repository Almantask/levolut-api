namespace BarberApi.Controllers
{
    public class AddBankFeeRuleRequest
    {
        public decimal Fee { get;  set; }
        public IEnumerable<string> BlockedCountries { get;  set; }
    }
}