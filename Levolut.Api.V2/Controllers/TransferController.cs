using Levolut.Api.V2.Contracts;
using Levolut.Api.V2.Contracts.Requests;
using Levolut.Api.V2.Contracts.Responses;
using Levolut.Api.V2.Services.Balance;
using Microsoft.AspNetCore.Mvc;

namespace Levolut.Api.V2.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class TransferController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        public TransferController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet("Balance/{bankId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBalanceResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBalance(long userId, long bankId)
        {
            var balance = _balanceService.GetCurrentBalance(userId, bankId);
            return Ok(new GetBalanceResponse(balance));
        }

        [HttpPost("Add/{bankId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddMoneyResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddMoney(long bankId, long userId, [FromBody] AddMoneyRequest request)
        {
            var balance = _balanceService.AddMoney(userId, bankId, request.MoneyExchange);
            return Ok(new AddMoneyResponse(balance));
        }

        [HttpPost("Cash/{bankId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CashMoneyResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CashMoney(long bankId, long userId, [FromBody] CashMoneyRequest request)
        {
            var balance = _balanceService.CashMoney(bankId, userId, request.MoneyExchange);
            return Ok(new CashMoneyResponse(balance));
        }
    }
}