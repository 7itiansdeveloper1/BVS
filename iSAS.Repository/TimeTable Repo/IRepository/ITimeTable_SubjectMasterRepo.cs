using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.TimeTable_Repo.IRepository
{
    public interface ITimeTable_SubjectMasterRepo
    {
        TimeTable_SubjectMasterModel TimeTable_SubjectMaster_Transaction();
        TimeTable_SubjectMasterModel TimeTable_SubjectMaster_Transaction(string subjectId);
        Tuple<int, string> TimeTable_SubjectMaster_CRUD(TimeTable_SubjectMasterModel model);
    }

}
