using Business_Logic_Layer.Services;
using Data_Access_Layer.DTO;
using Data_Access_Layer.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Validations
{
    public class RegisterValidator : AbstractValidator<User_DTO>
    {

        public RegisterValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz.")
                .MaximumLength(15).WithMessage("Kullanıcı adı alanı 15 karakteri geçemez.")
                .MinimumLength(4).WithMessage("Kullanıcı adı alanı 4 karakterden az olamaz");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı boş olamaz.")
                .MaximumLength(35).WithMessage("İsim alanı 35 karakteri geçemez")
                .MinimumLength(2).WithMessage("İsim alanı 2 karakterdenn az olamaz");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyadı alanı boş olamaz.")
                .MaximumLength(35).WithMessage("Soyadı alanı 35 karakteri geçemez")
                .MinimumLength(2).WithMessage("Soyadı alanı 2 karakterdenn az olamaz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola alanı boş olamaz.")
                .MaximumLength(35).WithMessage("Parola alanı 35 karakteri geçemez")
                .MinimumLength(6).WithMessage("Parola alanı 6 karakterdenn az olamaz");

        }
    }
}
