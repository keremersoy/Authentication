using Business_Logic_Layer.Services;
using Data_Access_Layer.DTO;
using Data_Access_Layer.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Validations
{
    public class LoginValidator : AbstractValidator<User_DTO>
    {
        public LoginValidator()
        {
            RuleFor(user => user.UserName)
            .NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz.")//.Must(BeUniqueUserName)
            .MinimumLength(3).WithMessage("Kullanıcı adı en az 4 karakter olmalıdır.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Parola alanı boş olamaz.")
                .MinimumLength(6).WithMessage("Parola en az 6 karakter olmalıdır");
        }
    }
}
