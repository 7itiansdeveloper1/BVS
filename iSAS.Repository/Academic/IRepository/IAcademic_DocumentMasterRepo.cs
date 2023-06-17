using System;
using System.Collections.Generic;
using ISas.Entities.Academic;
namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_DocumentMasterRepo
    {
        List<Academic_DocumentMasterModel> GetDocumentList(string docNo);
        Academic_DocumentMasterModel GetDocumentByDocNo(string docNo);
        Tuple<int, string> Academic_DocumentMaster_CRUD(Academic_DocumentMasterModel model);
        Tuple<int, string> Academic_DocumentMaster_CRUD(string docNo, string userId);
        
    }
}

