using CursusAdministratie2021.Server.Core.Interfaces;
using CursusAdministratie2021.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursusAdministratie2021.Server.Core.Services {
    public class EditionsService : IEditionsService {
        private readonly IEditionsRepository editionsRepository;

        public EditionsService(IEditionsRepository editionsRepository) {
            this.editionsRepository = editionsRepository;
        }

        public async Task<Edition> GetEditionWithStudents(int editionId) => await editionsRepository.GetEditionWithStudents(editionId);
    }
}
