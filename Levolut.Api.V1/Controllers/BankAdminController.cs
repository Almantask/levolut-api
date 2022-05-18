using Levolut.Api.V1.Contracts;
using Levolut.Api.V1.Infrastructure.Database;
using Levolut.Api.V1.Infrastructure.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAdminController : ControllerBase
    {
        private readonly ILogger<BankAdminController> _logger;
        private readonly LevolutDbContext context;

        public BankAdminController(ILogger<BankAdminController> logger, LevolutDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpPost(Name = "Rule/{bankId}")]
        public IActionResult AddRule(long bankId, AddBankFeeRuleRequest request)
        {
            var entity = context.BankFeeRules.Add(new BankFeeRule
            {
                BankId = bankId,
                Fee = request.Fee,
                BlockedCountries = request.BlockedCountries.Select(bc => new BlockedCountry { Name = bc }).ToList()
            });

            context.SaveChanges();

            return Ok(entity.Entity);
        }

        [HttpGet(Name = "Rule/{bankId}")]
        public IActionResult GetRules(long bankId)
        {
            var bankFeeRule = context.BankFeeRules
                .Include(i => i.BlockedCountries)
                .FirstOrDefault(bfr => bfr.BankId == bankId);

            if (bankFeeRule == null)
                return NotFound();

            return Ok(new GetBankFeeRulesResponse
            {
                BankFeeRule = bankFeeRule,
            });
        }
    }
}