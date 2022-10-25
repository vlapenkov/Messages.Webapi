using FluentValidation;
using Rk.Messages.Logic.SectionsNS.Commands.CreateSectionCommand;

namespace Rk.Messages.Logic.SectionsNS.Validations
{
    public class CreateSectionValidator :  AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Наименование раздела не должно быть пустым")
                .MinimumLength(5)
                .WithMessage("Минимальная длина раздела не менее 5 символов");
        }
    }
}
