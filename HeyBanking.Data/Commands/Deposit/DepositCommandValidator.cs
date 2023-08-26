using FluentValidation;
using HeyBanking.App.Commands.Deposit;

namespace HeyBanking.App.Commands.CreateAccount
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(v => v.Deposit)
                .GreaterThan(0)
                .LessThanOrEqualTo(10_000);
        }
    }
}
