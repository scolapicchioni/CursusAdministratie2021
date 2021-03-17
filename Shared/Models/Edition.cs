using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Models {
    public class Edition {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int CourseId { get; set; }
    }
}
