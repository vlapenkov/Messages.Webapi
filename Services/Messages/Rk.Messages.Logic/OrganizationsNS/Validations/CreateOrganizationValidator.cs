using System.Linq;
using FluentValidation;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;

namespace Rk.Messages.Logic.SectionsNS.Validations
{
    public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationRequest>
    {
        public CreateOrganizationValidator()
        {            

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Наименование организации не должно быть пустым")
                .MinimumLength(5)
                .WithMessage("Минимальная длина организации не менее 5 символов");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Полное наименование организации не должно быть пустым")
                .MinimumLength(5)
                .WithMessage("Минимальная длина организации не менее 5 символов");

            RuleFor(x => x.Ogrn)
                .NotEmpty()
                .WithMessage("ОГРН организации не должно быть пустым")
                .Matches("^\\d{13}$")
                .WithMessage("ОГРН организации 13 цифр");

            RuleFor(x => x.Inn)
                .NotEmpty()
                .WithMessage("ИНН организации не должно быть пустым")                
                .Matches("^\\d{10}$")
                .WithMessage("ИНН организации 10 цифр");

            RuleFor(x => x.Kpp)
                .NotEmpty()                
                .WithMessage("КПП организации 9 цифр")
                .Matches("^\\d{9}$")
                .WithMessage("КПП организации 9 цифр");

            RuleFor(x => x.Region)
                .NotEmpty()
                .WithMessage("Регион организации не должно быть пустым")
                .MinimumLength(5)
                .WithMessage("Минимальная длина региона организации не менее 5 символов");

            RuleFor(x => x.City)
               .NotEmpty()
               .WithMessage("Город организации не должно быть пустым");

            RuleFor(x => x.Address)
               .NotEmpty()
               .WithMessage("Адрес организации не должно быть пустым");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email организации должен быть корректным");
                       


        }
    }
}
