using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Utils
{
    class Validator : AbstractValidator<MVVM.ViewModel.RegViewModel>
    {
        public Validator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин не может быть пустым.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Заполните имя");

            RuleFor(x => x.SecondName)
                .NotEmpty().WithMessage("Заполните фамилию");

            RuleFor(x => x.PatronomycName)
                .NotEmpty().WithMessage("Заполните фамилию");

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Заполните почту")
                .EmailAddress().WithMessage("Введите существующую почту");

            RuleFor(x => x.Birthday)
                .NotEmpty().WithMessage("Заполните дату рождения");

        }
    }
}
