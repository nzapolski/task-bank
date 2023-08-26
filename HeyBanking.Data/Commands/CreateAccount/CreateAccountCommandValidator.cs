using FluentValidation;

namespace HeyBanking.App.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(v => v.InitialDeposit)
                .GreaterThanOrEqualTo(100);
        }
    }
}
