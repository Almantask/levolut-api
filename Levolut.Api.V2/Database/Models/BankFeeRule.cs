namespace Levolut.Api.V2.Database.Models
{
    public class BankFeeRule
    {
        public int Id { get; set; }
        public long BankId { get; internal set; }
        public decimal Fee { get; internal set; }
        // In general, avoid using concrete types if you don;t use anything concrete.
        // This shuld be IEnumerable instead.
        public List<BlockedCountry> BlockedCountries { get; internal set; }
    }
}