using HeyBanking.App.Commands.CreateAccount;
using HeyBanking.App.Commands.DeleteAccount;
using HeyBanking.App.Common.Models;
using HeyBanking.App.Queries.GetAccount;
using HeyBanking.App.Queries.GetAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeyBanking.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get paged list of accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PagedResult<AccountDto>>> GetAccounts([FromQuery] GetAccoutnsQuery query)
            => await _mediator.Send(query);

        /// <summary>
        /// Get account details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AccountDto>> GetAccount([FromRoute]Guid id)
            => await _mediator.Send(new GetAccoutnQuery
            {
                AccountId = id
            });

        /// <summary>
        /// Create new account with deposit
        /// </summary>
        /// <returns>account id</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAccount([FromBody] CreateAccountCommand command)
            => await _mediator.Send(command);

        /// <summary>
        /// Withdraw account balance and delete account
        /// </summary>
        /// <returns>account balance</returns>
        [HttpPost("withdrawAndDelete/{id:guid}")]
        public async Task<ActionResult<decimal>> WithdrawAndDeleteAccount([FromRoute] Guid id)
            => await _mediator.Send(new WithdrawAndDeleteAccountCommand
            {
                AccountId = id
            });
    }
}
