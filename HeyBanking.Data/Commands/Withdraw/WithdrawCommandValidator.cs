using FluentValidation;
using HeyBanking.App.Commands.CreateAccount;

namespace HeyBanking.App.Commands.Withdraw
{
    public class WithdrawCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public WithdrawCommandValidator()
        {
            RuleFor(v => v.InitialDeposit)
                .GreaterThan(0);
        }
    }
}
