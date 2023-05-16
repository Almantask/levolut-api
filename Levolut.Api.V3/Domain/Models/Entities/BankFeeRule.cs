namespace Levolut.Api.V3.Domain.Models.Entities
{
    public class BankFeeRule
    {
        public int Id { get; set; }
        public long BankId { get; internal set; }
        public decimal Fee { get; internal set; }
        public List<BlockedCountry> BlockedCountries { get; internal set; }
    }
}