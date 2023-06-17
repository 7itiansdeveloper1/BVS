using ISas.Entities.HR_Payroll_Entities;
using System;

namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_StaffEnrollmentRepo
    {
        HR_Payroll_StaffEnrollmentModels GetStaffEnrollmentDetails(string PayBandID, string PayBandName);
        Tuple<int, string> HR_Payroll_StaffEnrollment_CRUD(HR_Payroll_StaffEnrollmentModels model);
        Tuple<int, string> HR_Payroll_StaffEnrollment_CRUD(string PayBandID, string StaffID);
    }
}
