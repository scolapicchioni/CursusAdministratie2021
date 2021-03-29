using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.UnitTests.Builders.Models {
    public class CompanyEmployeeBuilder {
        private int _id;
        private string _name;
        private string _surname;
        private string _company;
        private string _department;
        private string _quotation;

        private CompanyEmployee companyEmployee;

        public static CompanyEmployeeBuilder Default() {
            return new CompanyEmployeeBuilder();
        }

        public CompanyEmployeeBuilder Simple() {
            return Default()
                .WithName("Not Interesting Name")
                .WithSurname("Not Interesting Surname")
                .WithCompany("Not Interesting Company");
        }

        public CompanyEmployeeBuilder Typical() {
            return Default()
                .Simple()
                .WithId(1)
                .WithDepartment("Not Interesting Department")
                .WithQuotation("Not Interesting Quotation");
        }

        public CompanyEmployee Build() {
            if (companyEmployee is null) {
                companyEmployee = new CompanyEmployee() {
                    Id = _id,
                    Name = _name,
                    Surname = _surname,
                    Company = _company,
                    Department = _department,
                    Quotation = _quotation
                };
            }
            return companyEmployee;
        }
        public CompanyEmployeeBuilder WithId(int id) {
            _id = id;
            return this;
        }
        public CompanyEmployeeBuilder WithName(string name) {
            _name = name;
            return this;
        }
        public CompanyEmployeeBuilder WithSurname(string surname) {
            _surname = surname;
            return this;
        }
        public CompanyEmployeeBuilder WithCompany(string company) {
            _company = company;
            return this;
        }
        public CompanyEmployeeBuilder WithDepartment(string department) {
            _department = department;
            return this;
        }
        public CompanyEmployeeBuilder WithQuotation(string quotation) {
            _quotation = quotation;
            return this;
        }
    }
}
