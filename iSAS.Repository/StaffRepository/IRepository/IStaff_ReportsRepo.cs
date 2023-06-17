using ISas.Entities.StaffEntities;
using System.Collections.Generic;

namespace ISas.Repository.StaffRepository.IRepository
{
    public interface IStaff_ReportsRepo
    {
        Staff_ReportsModels GetStaff_Reports_FormLoad();
        Staff_ReportsModels GetStaff_Report(string StaffIds, string Reporttype, bool InActive, string OrderBy, string ReportName, string ReportFilterBy);
    }
}
