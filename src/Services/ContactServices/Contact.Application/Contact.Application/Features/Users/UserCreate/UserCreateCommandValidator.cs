using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.UserCreate
{
    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator()
        { 

            RuleFor(p => p.name)
               .Cascade(CascadeMode.Stop)
               .NotNull()
               .NotEmpty()
               .WithMessage("name boş geçilemez.") 
               .MinimumLength(3).WithMessage("name en az 3 karakter olmalıdır")
               .MaximumLength(50).WithMessage("name en fazla 50 karakter olabilir");

            RuleFor(p => p.surname)
         .Cascade(CascadeMode.Stop)
         .NotNull()
         .NotEmpty()
         .WithMessage("surname boş geçilemez.")
         .MinimumLength(3).WithMessage("surname en az 3 karakter olmalıdır")
         .MaximumLength(50).WithMessage("surname en fazla 50 karakter olabilir");

            RuleFor(p => p.company)
         .Cascade(CascadeMode.Stop)
         .NotNull()
         .NotEmpty()
         .WithMessage("company boş geçilemez.")
         .MinimumLength(3).WithMessage("company en az 3 karakter olmalıdır")
         .MaximumLength(50).WithMessage("company en fazla 50 karakter olabilir");

        }
    }
}
