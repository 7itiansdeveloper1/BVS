using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_SetupRepo
    {
        List<Library_SetupModels> GetLibraryList(string LibraryId);
        Library_SetupModels GetLibraryById(string LibraryId);

        Tuple<int, string> Library_Setup_CRUD(Library_SetupModels model);
        Tuple<int, string> Library_Setup_CRUD(string LibraryId);
    }
}
