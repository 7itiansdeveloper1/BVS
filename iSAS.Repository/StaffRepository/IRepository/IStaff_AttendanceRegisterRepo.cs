using ISas.Entities.StaffEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.StaffRepository.IRepository
{
    public interface IStaff_AttendanceRegisterRepo
    {
        List<StaffAttendanceDetailsModel> GetStaffAttenDetails(string AttenDate, string DeptIDs);
        Tuple<int, string> Staff_AttendanceRegister_CRUD(Staff_AttendanceRegisterModels model);
    }
}
