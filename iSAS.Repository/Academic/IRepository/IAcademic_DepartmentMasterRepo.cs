using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_DepartmentMasterRepo
    {
        List<Academic_DepartmentMasterModels> GetDepartmentList();
        Tuple<int, string> Academic_DepartmentMaster_CRUD(Academic_DepartmentMasterModels model);
        Tuple<int, string> Academic_DepartmentMaster_CRUD(string DeptId);
    }
}
