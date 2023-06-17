using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.TimeTable_Repo.IRepository
{
    public interface ITimeTable_StaffWorkLoadSetupRepo
    {
        StaffClassSetupModels StaffClassSetup_FormLoad(string StaffId);
        Tuple<int, string> StaffClassSetup_CRUD(StaffClassSetupModels model);

        StaffSubjectSetupModels StaffSubjectSetup_FormLoad(string StaffId, string ClassSecId);
        Tuple<int, string> StaffSubjectClassSetup_CRUD(StaffSubjectSetupModels model);

        StaffSubjectSetupModels ExamSubjectSetup_FormLoad(string StaffId, string ClassSecId);
        Tuple<int, string> ExamSubjectClassSetup_CRUD(StaffSubjectSetupModels model);
    }
}
