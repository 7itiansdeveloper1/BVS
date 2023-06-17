using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_BookTitleMasterRepo
    {
        List<Library_BookTitleMasterModels> GetBookTitleList(string BookTitleId, string TitleType);
        Library_BookTitleMasterModels GetBookTitleById(string BookTitleId, string TitleType);

        Tuple<int, string> Library_BookTitleMaster_CRUD(Library_BookTitleMasterModels model);
        Tuple<int, string> Library_BookTitleMaster_CRUD(string BookTitleId);

        Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string TitleId, string TitleType);
    }
}
