using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.TimeTable_Repo.IRepository
{
    public interface ITimeTable_PeriodMatrixRepo
    {
        List<TimeTable_PeriodMatrixModels> GetMatrixList(string MatrixId);
        TimeTable_PeriodMatrixModels GetMatrixById(string MatrixId);
        Tuple<int, string> TimeTable_PeriodMatrix_CRUD(TimeTable_PeriodMatrixModels model);
        Tuple<int, string> TimeTable_PeriodMatrix_CRUD(string MatrixId);

        PeriodMatrixClassSetupModels PeriodMatrixClassSetup_FormLoad(string MatrixId);
        Tuple<int, string> PeriodMatrixClassSetup_CRUD(PeriodMatrixClassSetupModels model);
    }
}
