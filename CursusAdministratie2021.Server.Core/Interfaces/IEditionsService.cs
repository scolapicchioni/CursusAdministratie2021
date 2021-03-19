using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Core.Interfaces {
    public interface IEditionsService {
        public Task<Edition> GetEditionWithStudents(int editionId);
    }
}
