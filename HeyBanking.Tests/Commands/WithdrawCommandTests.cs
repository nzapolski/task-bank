using FluentAssertions;
using HeyBanking.App.Commands.CreateAccount;
using HeyBanking.App.Commands.Withdraw;
using HeyBanking.App.Common.Exceptions;
using HeyBanking.Domain.Entities;
using NUnit.Framework;

namespace HeyBanking.Tests.Commands
{
    using static Testing;

    [TestFixture]
    public class WithdrawCommandTests
    {
        [Test]
        public async Task ShouldRequireValidAmount()
        {
            var command = new WithdrawCommand {  Amount = -1 };
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldWithdraw()
        {
            var initialDeposit = 5_000M;
            var withdraw = 1_000M;

            var accountId = await SendAsync(new CreateAccountCommand
            {
                InitialDeposit = 5_000,
            });

            var command = new WithdrawCommand
            {
                AccountId = accountId,
                Amount = withdraw
            };

            await SendAsync(command);

            var item = await FindAsync<Account>(accountId);

            item.Should().NotBeNull();
            item!.Amount.Should().Be(initialDeposit - withdraw);
        }
    }
}
