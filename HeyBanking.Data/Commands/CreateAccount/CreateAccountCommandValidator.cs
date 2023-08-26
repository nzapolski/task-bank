using FluentValidation;

namespace HeyBanking.App.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(v => v.InitialDeposit)
                .GreaterThanOrEqualTo(100)
                .LessThanOrEqualTo(10_000)
                .WithMessage("Deposit must be from 100 to 10 000.");
        }
    }
}
