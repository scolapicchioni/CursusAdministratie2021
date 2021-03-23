using CursusAdministratie2021.Shared.Models;
using CursusAdministratie2021.Shared.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Shared.UnitTests.Validators {
    public class PrivateCitizenValidatorTests {
        [Fact]
        public void Validate_ShouldBeValid_WhenPrivateCitizenHasValidNameSurname() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { Name = "PrivateCitizenName", Surname = "PrivateCitizenSurname"};
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForName_WhenNameMissing() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("Name");
        }
        [Fact]
        public void Validate_ShouldHaveValidationErrorForName_WhenNameLongerThan200() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { Name = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("Name");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForName_WhenNameNotLongerThan200() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("Name");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForSurname_WhenSurnameMissing() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("Surname");
        }
        [Fact]
        public void Validate_ShouldHaveValidationErrorForSurname_WhenSurnameLongerThan200() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { Surname = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("Surname");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForSurname_WhenSurnameNotLongerThan200() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { Surname = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("Surname");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForStreetName_WhenStreetNameLongerThan200() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { StreetName = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("StreetName");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForStreetName_WhenStreetNameNotLongerThan200() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { StreetName = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("StreetName");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForHouseNumber_WhenHouseNumberLongerThan6() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { HouseNumber = "0123456" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("HouseNumber");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForHouseNumber_WhenHouseNumberNotLongerThan6() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { HouseNumber = "012345" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("HouseNumber");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForZipCode_WhenZipCodeLongerThan30() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { ZipCode = "0123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("ZipCode");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForZipCode_WhenZipCodeNotLongerThan30() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { ZipCode = "012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("ZipCode");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForCity_WhenCityLongerThan100() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { City = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("City");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForCity_WhenCityNotLongerThan100() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { City = "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("City");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForIBAN_WhenIBANLongerThan40() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { IBAN = "01234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("IBAN");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForIBAN_WhenIBANNotLongerThan40() {
            PrivateCitizenValidator validationRules = new PrivateCitizenValidator();
            PrivateCitizen privateCitizen = new PrivateCitizen() { IBAN = "0123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("IBAN");
        }
    }
}
