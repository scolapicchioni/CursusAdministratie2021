using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.Models {
    public class Course {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public virtual ICollection<Edition> Editions { get; set; }
    }
}
