using FluentValidation;

namespace ShoppingProject.Application.Identity.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage("Email boş olamaz.")
            .EmailAddress()
            .WithMessage("Geçerli bir email adresi girin.");

        RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage("Şifre boş olamaz.")
            .MinimumLength(6)
            .WithMessage("Şifre en az 6 karakter olmalıdır.");
    }
}
