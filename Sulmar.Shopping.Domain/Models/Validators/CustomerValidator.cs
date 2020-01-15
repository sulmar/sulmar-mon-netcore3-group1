using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.Shopping.Domain.Models.Validators
{

    // dotnet add package FluentValidation
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.Pesel).NotEmpty().Length(11);
            RuleFor(p => p.PostCode).Must((customer, property)
                => BeValidPostCode(property, customer.City))
                .WithMessage("Kod pocztowy niezgodny z miejscowością");
        }

        private bool BeValidPostCode(string postCode, string city)
        {
            return true;
        }

    }
}
