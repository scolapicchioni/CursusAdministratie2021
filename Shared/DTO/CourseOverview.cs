using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Shared.DTO {
    public class CourseOverview {
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public int EditionId { get; set; }
        public int EnrollmentsCount { get; set; }
    }
}
