using CursusAdministratie2021.Shared.Models;
using CursusAdministratie2021.Shared.Validators;
using CursusAdministratie2021.UnitTests.Builders.Models;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CursusAdministratie2021.Shared.UnitTests.Validators {

    public class PrivateCitizenValidatorTests {
        private PrivateCitizenValidator validationRules;
        private readonly PrivateCitizenBuilder privateCitizenBuilder;
        private PrivateCitizen privateCitizen;

        public PrivateCitizenValidatorTests() {
            validationRules = new PrivateCitizenValidator();
            privateCitizenBuilder = PrivateCitizenBuilder.Default();      
        }
        [Fact]
        public void Validate_ShouldBeValid_WhenPrivateCitizenHasValidNameSurname() {
            privateCitizen = privateCitizenBuilder.Simple().Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForName_WhenNameMissing() {
            privateCitizen = privateCitizenBuilder.Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("Name");
        }
        [Fact]
        public void Validate_ShouldHaveValidationErrorForName_WhenNameLongerThan200() {
            privateCitizen = privateCitizenBuilder.WithName("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("Name");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForName_WhenNameNotLongerThan200() {
            privateCitizen = privateCitizenBuilder.WithName("01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("Name");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForSurname_WhenSurnameMissing() {
            privateCitizen = privateCitizenBuilder.Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("Surname");
        }
        [Fact]
        public void Validate_ShouldHaveValidationErrorForSurname_WhenSurnameLongerThan200() {
            privateCitizen = privateCitizenBuilder.WithSurname("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("Surname");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForSurname_WhenSurnameNotLongerThan200() {
            privateCitizen = privateCitizenBuilder.WithSurname("01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("Surname");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForStreetName_WhenStreetNameLongerThan200() {
            privateCitizen = privateCitizenBuilder.WithStreetName("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("StreetName");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForStreetName_WhenStreetNameNotLongerThan200() {
            privateCitizen = privateCitizenBuilder.WithStreetName("01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("StreetName");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForHouseNumber_WhenHouseNumberLongerThan6() {
            privateCitizen = privateCitizenBuilder.WithHouseNumber("0123456")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("HouseNumber");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForHouseNumber_WhenHouseNumberNotLongerThan6() {
            privateCitizen = privateCitizenBuilder.WithHouseNumber("012345")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("HouseNumber");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForZipCode_WhenZipCodeLongerThan30() {
            privateCitizen = privateCitizenBuilder.WithZipCode("0123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("ZipCode");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForZipCode_WhenZipCodeNotLongerThan30() {
            privateCitizen = privateCitizenBuilder.WithZipCode("012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("ZipCode");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForCity_WhenCityLongerThan100() {
            privateCitizen = privateCitizenBuilder.WithCity("01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("City");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForCity_WhenCityNotLongerThan100() {
            privateCitizen = privateCitizenBuilder.WithCity("0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("City");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForIBAN_WhenIBANLongerThan40() {
            privateCitizen = privateCitizenBuilder.WithIBAN("01234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldHaveValidationErrorFor("IBAN");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForIBAN_WhenIBANNotLongerThan40() {
            privateCitizen = privateCitizenBuilder.WithIBAN("0123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(privateCitizen);
            results.ShouldNotHaveValidationErrorFor("IBAN");
        }
    }
}
