using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using HeyBanking.App.Commands.CreateAccount;
using HeyBanking.App.Common.Exceptions;
using HeyBanking.Domain.Entities;
using NUnit.Framework;

namespace HeyBanking.Tests.Commands
{
    using static Testing;

    [TestFixture]
    public class CreateAccountTests
    {
        [Test]
        public async Task ShouldRequireInitialDepositGreaterZero()
        {
            var command = new CreateAccountCommand
            {
                InitialDeposit = 50
            };

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTodoItem()
        {
            var ammount = 5_000;

            var command = new CreateAccountCommand
            {
                InitialDeposit = ammount
            };

            var accountId = await SendAsync(command);

            var item = await FindAsync<Account>(accountId);

            item.Should().NotBeNull();
            item!.Amount.Should().Be(ammount);
            item.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(2000));
        }
    }
}
