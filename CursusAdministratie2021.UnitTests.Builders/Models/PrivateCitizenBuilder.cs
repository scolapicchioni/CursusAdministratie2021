using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.UnitTests.Builders.Models {
    public class PrivateCitizenBuilder {
        private int _id;
        private string _name;
        private string _surname;
        private string _streetName;
        private string _houseNumber;
        private string _city;
        private string _zipCode;
        private string _iban;

        private PrivateCitizen privateCitizen;

        public static PrivateCitizenBuilder Default() {
            return new PrivateCitizenBuilder();
        }

        public PrivateCitizenBuilder Simple() {
            return Default()
                .WithName("Not Interesting Name")
                .WithSurname("Not Interesting Surname");
        }

        public PrivateCitizenBuilder Typical() {
            return Default()
                .Simple()
                .WithId(1)
                .WithStreetName("Not Interesting StreetName")
                .WithHouseNumber("NIHN")
                .WithCity("Not Interesting City")
                .WithZipCode("Not Interesting ZC")
                .WithIBAN("Not Interesting IBAN");
        }

        public PrivateCitizen Build() {
            if (privateCitizen is null) {
                privateCitizen = new PrivateCitizen() {
                    Id = _id,
                    Name = _name,
                    Surname = _surname,
                    StreetName = _streetName,
                    HouseNumber = _houseNumber,
                    City = _city,
                    ZipCode = _zipCode,
                    IBAN = _iban
                };
            }
            return privateCitizen;
        }
        public PrivateCitizenBuilder WithId(int id) {
            _id = id;
            return this;
        }
        public PrivateCitizenBuilder WithName(string name) {
            _name = name;
            return this;
        }
        public PrivateCitizenBuilder WithSurname(string surname) {
            _surname = surname;
            return this;
        }
        public PrivateCitizenBuilder WithStreetName(string streetName) {
            _streetName = streetName;
            return this;
        }
        public PrivateCitizenBuilder WithHouseNumber(string houseNumber) {
            _houseNumber = houseNumber;
            return this;
        }
        public PrivateCitizenBuilder WithCity(string city) {
            _city = city;
            return this;
        }
        public PrivateCitizenBuilder WithZipCode(string zipCode) {
            _zipCode = zipCode;
            return this;
        }
        public PrivateCitizenBuilder WithIBAN(string iban) {
            _iban = iban;
            return this;
        }
    }
}
