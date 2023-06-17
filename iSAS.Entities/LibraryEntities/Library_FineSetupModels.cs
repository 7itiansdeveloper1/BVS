using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.LibraryEntities
{
    public class Library_FineSetupModels
    {
        public string FineId { get; set; }
        public string LibraryId { get; set; }
        public string LibraryName { get; set; }
        public int FineRateForStudent { get; set; }
        public int FineRateForStaff { get; set; }
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
