
namespace ISas.Entities
{
    
    //public class ClassUpdationList
    //{
    //    public string StudentERPNo { get; set; }
    //    public string StudentMorningAttendnace { get; set; }
    //    public string StudentAfternoonAttendnace { get; set; }
    //    public string StudentLeaveType { get; set; }
    //}
    
    public class StudentUpdate_CRUD
    {
        public string StudentERPNo { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
        public string Parameter3 { get; set; }
    }
    public class UpdationParametersList
    {
        public string FieldName { get; set; }
        public string FieldDisplayName { get; set; }
    }
    public class ClassofJoiningList
    {
        public Student Student { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
        public string Parameter3 { get; set; }

        public string DD1TextVal { get; set; }
        public string DD2TextVal { get; set; }
        public string DD3TextVal { get; set; }
    }
    public class ReligionList
    {
        public string ReligionId { get; set; }
        public string ReligionName { get; set; }
        public bool IsDefault { get; set; }
        public int PrintOrder { get; set; }
    }
    public class StreamList
    {
        public string SteremId { get; set; }
        public string StreamName { get; set; }
        public bool IsDefault { get; set; }
        public int PrintOrder { get; set; }
    }
    public class CategoryList1
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDefault { get; set; }
        public int PrintOrder { get; set; }
    }
    public class HouseList
    {
        public string HouseId { get; set; }
        public string HouseName { get; set; }
        public int PrintOrder { get; set; }
    }
    public class BloodGroupList
    {
        public string BloodGroupId { get; set; }
        public string BloodGroupName { get; set; }
        public bool IsDefault { get; set; }
        public int PrintOrder { get; set; }
    }
    public class ProfessionList
    {
        public string ProfessionId { get; set; }
        public string ProfessionName { get; set; }
        public int PrintOrder { get; set; }
    }
    public class ModeofTransportList
    {
        public string ModeofTransportId { get; set; }
        public string ModeofTransportName { get; set; }
        public int PrintOrder { get; set; }
    }
    public class PickedUpByList
    {
        public string PickedUpId { get; set; }
        public string PickedUpBy { get; set; }
        public int PrintOrder { get; set; }
    }
    //public class DailyAttenanceSummary
    //{
    //    public string ClassName { get; set; }
    //    public string ClassTeacherName { get; set; }
    //    public string AttendanceMarkedBy { get; set; }
    //    public string AttendanceTime { get; set; }
    //    public int ClassTotalStrength { get; set; }
    //    public int ClassPresentStudentCount { get; set; }
    //    public int ClassAbsentStudentCount { get; set; }
    //    public int ClassLeaveStudentCount { get; set; }
    //    public int ClassPrintOrder { get; set; }
    //    public int SectionPrintOrder { get; set; }
    //}
}
