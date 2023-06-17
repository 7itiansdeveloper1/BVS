using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_PublisherMasterRepo
    {
        List<Library_PublisherMasterModels> GetPublisherList(string PublisherId);
        Library_PublisherMasterModels GetPublisherById(string PublisherId);

        Tuple<int, string> Library_PublisherMaster_CRUD(Library_PublisherMasterModels model);
        Tuple<int, string> Library_PublisherMaster_CRUD(string PublisherId);

        Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string PublisherId);
    }
}
