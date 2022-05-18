﻿namespace BarberApi.Controllers
{
    public class UpdateBankFeeRuleRequest
    {
        public IEnumerable<string> BlockedCountries { get; set; }
        public decimal Fee { get; set; }
        public int FeeRuleId { get; set; }
    }
}