using FluentValidation;

public class GuessValidator : AbstractValidator<string>
{
    public GuessValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Your guess cannot be empty.")
            .Length(1).WithMessage("You can only guess one letter at a time.")
            .Matches("^[a-zA-Z]$").WithMessage("Only alphabetic characters are allowed.");
    }
}
