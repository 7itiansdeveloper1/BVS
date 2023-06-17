using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_AuthorMasterRepo
    {
        List<Library_AuthorMasterModels> GetAuthorList(string AuthorId, string AuthorType);
        Library_AuthorMasterModels GetAuthorById(string AuthorId, string AuthorType);

        Tuple<int, string> Library_AuthorMaster_CRUD(Library_AuthorMasterModels model);
        Tuple<int, string> Library_AuthorMaster_CRUD(string AuthorId);


        Library_Author_IndividualReportModel Get_IndividualReport(string AuthorType, string AuthorId);
        Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string AuthorId, string AuthorType, string TitleId);
    }
}
