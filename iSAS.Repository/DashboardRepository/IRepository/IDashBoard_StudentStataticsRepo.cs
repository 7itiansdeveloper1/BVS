using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IDashBoard_StudentStataticsRepo
    {
        #region  Admission Details
        StudentAdmissionDetailsMainModels GetStudentAdmissionDetails();
        List<_StudentDetails> _GetStudentAdmissionDetails(string ClassSectionId, string StaticMode);
        List<StudentAdmissionDetailsSubModel> GetStudentAdmissionDetails_Charts();
        #endregion

        #region  FeeCollection Details
        FeeCollectionDetailsModel GetFeeCollectionDetails();
        FeeCollectionDetailsModel GetFeeCollectionDetails_Charts();

        List<_StudentDetails> _GetFeeCollectionDetails(string ClassSectionId, string StaticMode, string SessionId);

        List<_StudentDetails> _GetDefaulterOrBalanceDetails(string ClassSectionId, string SessionId);
        #endregion

        StudentDozearModel GetStudentDozear(string erpNo, string SessionId);
        List<AttendanceDetailModel> StudentAttendanceDetails(string erpNo, string SessionId);
        List<StudentDetailsModel> GetClassStudentDetails(string classSectionId, string SessionId);
        List<StudentDetailsModel> GetFeeDefaulterDetails(string classSectionId, string SessionId);
    }
}
