using FluentValidation;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Validations
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
