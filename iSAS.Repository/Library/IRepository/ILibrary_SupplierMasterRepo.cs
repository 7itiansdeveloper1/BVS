using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_SupplierMasterRepo
    {
        List<Library_SupplierMasterModels> GetSupplierList(string SupplierId);
        Library_SupplierMasterModels GetSupplierById(string SupplierId);

        Tuple<int, string> Library_SupplierMaster_CRUD(Library_SupplierMasterModels model);
        Tuple<int, string> Library_SupplierMaster_CRUD(string SupplierId);

        Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string SupplierId);
    }
}
