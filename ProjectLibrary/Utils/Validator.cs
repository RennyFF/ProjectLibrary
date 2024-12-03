using FluentValidation;
using ProjectLibrary.MVVM.ViewModel.CoreVMs;

namespace ProjectLibrary.Utils
{
    class Validator : AbstractValidator<RegViewModel>
    {
        Npgsql.NpgsqlConnection ConnectionDB { get; set; }
        public Validator(Npgsql.NpgsqlConnection Connection)
        {
            ConnectionDB = Connection;

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Поле не может быть пустым.")
                .MustAsync(async (Login, cancellation) =>
                {
                    return await MVVM.Model.DataBaseFunctions.CheckIfUnique(ConnectionDB, true, Login);
                }).WithMessage("Логин уже занят."); ;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Поле не может быть пустым.");

            RuleFor(x => x.SecondName)
                .NotEmpty().WithMessage("Поле не может быть пустым.");

            RuleFor(x => x.PatronomycName)
                .NotEmpty().WithMessage("Поле не может быть пустым.");

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Поле не может быть пустым.")
                .EmailAddress().WithMessage("Введите существующую почту.")
                .MustAsync(async (Mail, cancellation) =>
                {
                    return await MVVM.Model.DataBaseFunctions.CheckIfUnique(ConnectionDB, false, Mail);
                }).WithMessage("Почта уже занята.");

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
        private bool BeAValidDate(DateTime CheckingDate)
        {
            return CheckingDate < DateTime.Now ? true : false;
        }

    }
}
