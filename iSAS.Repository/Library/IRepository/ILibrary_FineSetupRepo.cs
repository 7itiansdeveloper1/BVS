using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_FineSetupRepo
    {
        //List<SelectListItem> Get_LibraryList(string UserId);
        List<Library_FineSetupModels> Get_FinSetupList(string LibraryId, string FineId, string UserId);
        Library_FineSetupModels Get_FinSetupById(string LibraryId, string FineId, string UserId);
        Tuple<int, string> Library_FineSetup_CRUD(Library_FineSetupModels model);
        Tuple<int, string> Library_FineSetup_CRUD(string FineId);
    }
}
