using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_SubjectMasterRepo 
    {
        List<Library_SubjectMasterModels> GetSubjectList(string SubjectId);
        Library_SubjectMasterModels GetSubjectById(string SubjectId);

        Tuple<int, string> Library_SubjectMaster_CRUD(Library_SubjectMasterModels model);
        Tuple<int, string> Library_SubjectMaster_CRUD(string SubjectId);

        Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string SubjectId);
    }
}
