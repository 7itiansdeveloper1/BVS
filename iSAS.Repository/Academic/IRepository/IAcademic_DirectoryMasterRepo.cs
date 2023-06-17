using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_DirectoryMasterRepo
    {
        List<Academic_DirectoryMasterModels> Get_DirectoryList(string DirectoryId, string UserId);
        Academic_DirectoryMasterModels Get_DirectoryById(string DirectoryId, string UserId);
        Tuple<int, string> Academic_DirectoryMaster_CRUD(Academic_DirectoryMasterModels model);
        Tuple<int, string> Academic_DirectoryMaster_CRUD(string DirectoryId);
    }
}
