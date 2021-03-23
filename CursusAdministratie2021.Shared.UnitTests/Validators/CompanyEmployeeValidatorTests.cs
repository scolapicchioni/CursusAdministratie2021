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
    public class CompanyEmployeeValidatorTests {
        [Fact]
        public void Validate_ShouldBeValid_WhenCompanyEmployeeHasValidNameSurnameCompany() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Name = "CompanyEmployeeName", Surname = "CompanyEmployeeSurname", Company="CompanyName" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForName_WhenNameMissing() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Name");
        }
        [Fact]
        public void Validate_ShouldHaveValidationErrorForName_WhenNameLongerThan200() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Name = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Name");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForName_WhenNameNotLongerThan200() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Name = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Name");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForSurname_WhenSurnameMissing() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Surname");
        }
        [Fact]
        public void Validate_ShouldHaveValidationErrorForSurname_WhenSurnameLongerThan200() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Surname = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Surname");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForSurname_WhenSurnameNotLongerThan200() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Surname = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Surname");
        }
        
        [Fact]
        public void Validate_ShouldHaveValidationErrorForCompany_WhenCompanyLongerThan200() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Company = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Company");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForCompany_WhenCompanyNotLongerThan200() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Company = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Company");
        }
        
        [Fact]
        public void Validate_ShouldHaveValidationErrorForDepartment_WhenDepartmentLongerThan200() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Department = "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Department");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForDepartment_WhenDepartmentNotLongerThan200() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Department = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Department");
        }

        [Fact]
        public void Validate_ShouldHaveValidationErrorForQuotation_WhenQuotationLongerThan30() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Quotation = "0123456789012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldHaveValidationErrorFor("Quotation");
        }
        [Fact]
        public void Validate_ShouldNotHaveValidationErrorForQuotation_WhenQuotationNotLongerThan30() {
            CompanyEmployeeValidator validationRules = new CompanyEmployeeValidator();
            CompanyEmployee companyEmployee = new CompanyEmployee() { Quotation = "012345678901234567890123456789" };
            var results = validationRules.TestValidate(companyEmployee);
            results.ShouldNotHaveValidationErrorFor("Quotation");
        }
    }
}
