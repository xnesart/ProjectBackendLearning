using FluentValidation;
using ProjectBackendLearning.Core.Models.Requests;

namespace ProjectBackendLearning.Core.Validation;

public class UserUpdateValidator : AbstractValidator<UpdateUserRequest>
{
    public UserUpdateValidator()
    {
        RuleFor(u => u.UserName).NotEmpty().NotNull().WithMessage("Заполните имя пользователя");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Почта не должна быть пустой").EmailAddress()
            .WithMessage("Почта не должна быть пустой");
        RuleFor(u => u.Age).NotEmpty().WithMessage("Возраст не должен быть пустым")
            .GreaterThan(17)
            .WithMessage("Возраст должен быть больше 18")
            .LessThan(200).WithMessage("Возраст должен быть меньше 200");
    }
}