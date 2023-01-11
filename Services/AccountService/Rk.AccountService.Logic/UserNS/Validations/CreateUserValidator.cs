using FluentValidation;
using Rk.AccountService.Logic.UserNS.Commands.CreateUser;
using Rk.Messages.Common.Helpers;

namespace Rk.AccountService.Logic.UserNS.Validations;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Request)
            .NotNull()
            .WithMessage("Запрос не может быть пустым");

        RuleFor(x => x.Request.FirstName)
            .NotEmpty()
            .WithMessage("Имя не должно быть пустым");
        
        RuleFor(x => x.Request.LastName)
            .NotEmpty()
            .WithMessage("Фамилия не должна быть пустой");
        
        RuleFor(x => x.Request.Attributes.Patronymic)
            .NotEmpty()
            .WithMessage("Отчество не должно быть пустым");
        
        RuleFor(x => x.Request.Username)
            .NotEmpty()
            .WithMessage("Имя пользователя не должно быть пустым");
        
        RuleFor(x => x.Request.Email)
            .NotEmpty()
            .WithMessage("Электронная почта не должна быть пустой");

        RuleFor(x => x.Request).Custom((request, context) =>
        {
            if(!request.Credentials.IsAny())
                context.AddFailure($"Реквизиты для входа не должны быть пустыми");
            
            if (request.Credentials.Any())
            {
                var crd = request.Credentials[0];
                if(string.Equals(crd.Type, "password", StringComparison.InvariantCultureIgnoreCase) && string.IsNullOrWhiteSpace(crd.Value))
                    context.AddFailure($"Пароль не должен быть пустым");
            }
        });
    }
}