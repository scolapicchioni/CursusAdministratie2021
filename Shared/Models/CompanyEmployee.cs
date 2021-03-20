using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Models {
    public class CompanyEmployee : Student {
        public string Company { get; set; }
        public string Department { get; set; }
        public string Quotation { get; set; }
    }
}
