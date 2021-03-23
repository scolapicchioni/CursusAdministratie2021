using CursusAdministratie2021.Shared.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Validators {
    public class PrivateCitizenValidator : AbstractValidator<PrivateCitizen> {
        public PrivateCitizenValidator() {
            RuleFor(privateCitizen => privateCitizen.Name).MaximumLength(200).NotNull();
            RuleFor(privateCitizen => privateCitizen.Surname).MaximumLength(200).NotNull();
            RuleFor(privateCitizen => privateCitizen.StreetName).MaximumLength(200);
            RuleFor(privateCitizen => privateCitizen.HouseNumber).MaximumLength(6);
            RuleFor(privateCitizen => privateCitizen.ZipCode).MaximumLength(30);
            RuleFor(privateCitizen => privateCitizen.City).MaximumLength(100);
            RuleFor(privateCitizen => privateCitizen.IBAN).MaximumLength(40);
        }
    }
}
