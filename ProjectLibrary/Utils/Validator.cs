using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectLibrary.Utils
{
    class Validator : AbstractValidator<MVVM.ViewModel.RegViewModel>
    {
        public Validator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Поле не может быть пустым.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Поле не может быть пустым.");

            RuleFor(x => x.SecondName)
                .NotEmpty().WithMessage("Поле не может быть пустым.");

            RuleFor(x => x.PatronomycName)
                .NotEmpty().WithMessage("Поле не может быть пустым.");

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Поле не может быть пустым.")
                .EmailAddress().WithMessage("Введите существующую почту.");

            RuleFor(x => x.Birthday)
                .Must(BeAValidDate)
                .WithMessage("Введена неправильная дата.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Поле не может быть пустым.")
                .MinimumLength(8).WithMessage("Пароль должен быть больше 8 символов.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Поле не может быть пустым.")
                .Must((x, confirmPassword) => confirmPassword == x.Password).WithMessage("Пароли не совпадают."); ;

        }
        //Make async Validation to check unique Login and Mail
        private bool BeAValidDate(DateTime value)
        {
            return value < DateTime.Now ? true: false;
        }
    }
}
