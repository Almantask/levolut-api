using Levolut.Api.V3.Domain.Services.BankFee;
using Levolut.Api.V3.Infrastructure.API.Contracts.Requests;
using Levolut.Api.V3.Infrastructure.API.Contracts.Responses;
using Levolut.Api.V3.Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Mvc;
// What is the responsibility of a controller?
// What code should it contain and should this be here?

namespace Levolut.Api.V3.Infrastructure.API.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class BankAdminController : ControllerBase
    {
        private readonly IBankFeeService bankFeeSerivce;

        public BankAdminController(IBankFeeService bankFeeSerivce)
        {
            this.bankFeeSerivce = bankFeeSerivce;
        }

        [HttpGet(Name = "Rule/{bankId}")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBankFeeRulesResponse))]
        public IActionResult GetRules(long bankId)
        {
            // Mention that it could be improved by having 3 different kinds of models:
            // Db, Domain, Api.

            // What are the alternatives to services?
            // Services, mediator, use case object.
            var bankFeeRule = bankFeeSerivce.GetBankFeeRule(bankId);

            return Ok(new GetBankFeeRulesResponse(bankFeeRule));
        }

        [HttpPost(Name = "Rule/{bankId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankFeeRule))]
        public IActionResult AddRule(long bankId, AddBankFeeRuleRequest request)
        {
            // We could call query handler directly, because that's all it does NOW.
            // Should we allow ourselves to do that?

            // This is a code smell because it does a little bit too much already.
            // We should use automapper or similar.
            var bankFeeRule = new BankFeeRule
            {
                BankId = bankId,
                Fee = request.Fee,
                BlockedCountries = request.BlockedCountries.Select(bc => new BlockedCountry { Name = bc }).ToList()
            };
            var entity = bankFeeSerivce.AddBankFeeRule(bankFeeRule);
            return Ok(entity);
        }
    }
}