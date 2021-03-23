using CursusAdministratie2021.Shared.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Validators {
    public class CompanyEmployeeValidator : AbstractValidator<CompanyEmployee> {
        public CompanyEmployeeValidator() {
            RuleFor(companyEmployee => companyEmployee.Name).MaximumLength(200).NotNull();
            RuleFor(companyEmployee => companyEmployee.Surname).MaximumLength(200).NotNull();
            RuleFor(companyEmployee => companyEmployee.Company).MaximumLength(200).NotNull();
            RuleFor(companyEmployee => companyEmployee.Department).MaximumLength(200);
            RuleFor(companyEmployee => companyEmployee.Quotation).MaximumLength(30);
        }
    }
}
