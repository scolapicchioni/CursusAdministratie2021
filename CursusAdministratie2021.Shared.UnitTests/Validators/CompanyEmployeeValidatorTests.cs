using CursusAdministratie2021.Shared.Models;
using CursusAdministratie2021.Shared.Validators;
using CursusAdministratie2021.UnitTests.Builders.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace CursusAdministratie2021.Shared.UnitTests.Validators {
    public class CompanyEmployeeValidatorTests {
        private CompanyEmployeeValidator validationRules;
        CompanyEmployeeBuilder employeeBuilder;
        CompanyEmployee companyEmployee;

        public CompanyEmployeeValidatorTests() {
            validationRules = new();
            employeeBuilder = CompanyEmployeeBuilder.Default();
        }

        [Fact]
        public void Validate_ShouldBeValid_WhenCompanyEmployeeHasValidNameSurnameCompany() {
            companyEmployee = employeeBuilder.Simple().Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForName_WhenNameMissing() {
            companyEmployee = employeeBuilder.Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Name");
        }
        [Fact]
        public void Validate_ShouldHaveValidationErrorForName_WhenNameLongerThan200() {
            companyEmployee = employeeBuilder
                .WithName("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Name");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForName_WhenNameNotLongerThan200() {
            companyEmployee = employeeBuilder.WithName("01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Name");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForSurname_WhenSurnameMissing() {
            companyEmployee = employeeBuilder.Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Surname");
        }
        [Fact]
        public void Validate_ShouldHaveValidationErrorForSurname_WhenSurnameLongerThan200() {
            companyEmployee = employeeBuilder.WithSurname("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Surname");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForSurname_WhenSurnameNotLongerThan200() {
            companyEmployee = employeeBuilder.WithSurname("01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Surname");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForCompany_WhenCompanyLongerThan200() {
            companyEmployee = employeeBuilder.WithCompany("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Company");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForCompany_WhenCompanyNotLongerThan200() {
            companyEmployee = employeeBuilder.WithCompany("01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Company");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForDepartment_WhenDepartmentLongerThan200() {
            companyEmployee = employeeBuilder.WithDepartment("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Department");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForDepartment_WhenDepartmentNotLongerThan200() {
            companyEmployee = employeeBuilder.WithDepartment("01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Department");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForQuotation_WhenQuotationLongerThan30() {
            companyEmployee = employeeBuilder.WithQuotation("0123456789012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Quotation");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForQuotation_WhenQuotationNotLongerThan30() {
            companyEmployee = employeeBuilder.WithQuotation("012345678901234567890123456789")
                .Build();
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Quotation");
        }
    }
}
