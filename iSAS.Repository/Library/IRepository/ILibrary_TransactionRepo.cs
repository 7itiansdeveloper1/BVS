using ISas.Entities.LibraryEntities;
using System;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_TransactionRepo
    {
        Library_Transaction_BookDetailsModels GetBookDetails(string AccNo, string LibID);
        Tuple<int, string> Library_Transaction_CRUD(Library_TransactionModels model);
        Library_TransactionModels GetReturnBookDetails(string LibId, string ERPNo, string ReturnDate);
    }
}
