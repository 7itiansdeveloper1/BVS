using ISas.Entities.LibraryEntities;
using System;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_BookMasterRepo
    {
        Library_BookMasterModels GetBookById(string LibId, string AccNo, string BtnClick);
        Library_BookMasterModels GetFormLoadDetails();
        Tuple<int, string> Library_BookMaster_CRUD(Library_BookMasterModels model);
    }
}
