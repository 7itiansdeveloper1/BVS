using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.StaffEntities;
namespace ISas.Repository.StaffRepository.IRepository
{
    public interface IStaff_DocumentUploadRepo
    {
        Staff_DocumentUploadModels Staff_DocumentUpload_Transaction(string staffId, string staffName);
        Tuple<int, string> StaffDocumentUpload_CRUD(string staffId, string docId, string docPath,string userId,string docAlias,string filename,string certificationDate, string trainedby);
        //List<StaffDocumentList> Staff_DocumentUpload_Transaction(string staffId);
        Tuple<int, string> StaffDocumentUpload_DELETE(int docno);
    }
}
