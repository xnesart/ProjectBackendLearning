using FluentValidation;
using ProjectBackendLearning.Core.Models.Requests;

namespace ProjectBackendLearning.Core.Validation;

public class UserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(u => u.UserName).NotEmpty().NotNull().WithMessage("Заполните имя пользователя");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Почта не должна быть пустой").EmailAddress()
            .WithMessage("Почта не должна быть пустой");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Пароль не должен быть пустым")
            .MinimumLength(8).WithMessage("Длина пароля должна быть минимум 8 символов.")
            .MaximumLength(16).WithMessage("Длина пароля должна быть максимум 16 символов.")
            .Matches(@"[A-Z]+").WithMessage("Пароль должен содержать хотя бы 1 заглавную букву")
            .Matches(@"[a-z]+").WithMessage("Пароль должен содержать хотя бы 1 строчную букву.")
            .Matches(@"[0-9]+").WithMessage("Пароль должен содержать хотя бы 1 цифру")
            .Matches(@"[\!\?\*\.]+").WithMessage("ароль должен содержать хотя бы 1 символ (!? *.).");
        RuleFor(u => u.Age).NotEmpty().WithMessage("Возраст не должен быть пустым");
        RuleFor(u => u.Age).GreaterThan(17).WithMessage("Возраст должен быть больше 18");
        RuleFor(u => u.Age).LessThan(200).WithMessage("Возраст должен быть меньше 200");
    }
}