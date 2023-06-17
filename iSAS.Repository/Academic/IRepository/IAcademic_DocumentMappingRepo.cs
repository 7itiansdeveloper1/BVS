using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_DocumentMappingRepo 
    {
        //Academic_DocumentMappingModel DocumentMapping_Transaction();
        List<SelectListItem> DocumentMapping_Transaction_DepartmentList();
        //List<DocumentListModel> DocumentMapping_Transaction_DocumentList(string deptId);
        Academic_DocumentMappingModel DocumentMapping_Transaction_DocumentList(string deptId);
        Tuple<int, string> DocumentMapping_CRUD(string departmentId, string documentsIds, string userId);
    }
}
