using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_SyllabusMasterRepo
    {
        List<Academic_SyllabusMasterModels> Get_Academic_SyllabusMasterList(string UserId);
        Academic_SyllabusMasterModels Get_SyllabusMasterById(string SyllabusId, string UserId);
        Academic_SyllabusMasterModels Get_Academic_SyllabusMaster_FormLoad(string UserId);
        Tuple<int, string> Academic_SyllabusMaster_CRUD(Academic_SyllabusMasterModels model);
        Tuple<int, string> Academic_SyllabusMaster_CRUD(string SyllabusId, string ToBeRemovedAttach, string AllAttachments, string UserId);
        List<Academic_SyllabusMasterModels> Get_Academic_SyllabusMasterList(string ClassSectionId, string ErpNo);
    }
}
