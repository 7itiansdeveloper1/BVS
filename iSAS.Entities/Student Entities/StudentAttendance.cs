
namespace ISas.Entities
{
    public enum LeaveType
    {
        NA = 0,
        SL = 1,
        ML = 2,
        PL = 3
    }

    public enum AttendanceType
    {
        L = 0,
        P = 1,
        A = 2
    }
    public class ClassAttendance
    {
        public string StudentERPNo { get; set; }
        
        public string StudentMorningAttendnace { get; set; }
        
        public string StudentAfternoonAttendnace { get; set; }
        
        public string StudentLeaveType { get; set; }
    }
    public class StudentAttendnaceDetails
    {
        public string MorningAttendnace { get; set; }

        public string AfternoonAttendnace { get; set; }

        public string LeaveType { get; set; }
    }
    public class StudentAttendance
    {
        public Student Student { get; set; }

        //public LeaveType Leave { get; set; }

        public StudentAttendnaceDetails StudentAttendanceDetails { get; set; }

        //public AttendanceType MorningAttendance { get; set; }

        //public AttendanceType AfterAttendance { get; set; }
        
    }
    public class DailyAttenanceSummary
    {
        public string ClassName { get; set; }
        public string ClassTeacherName { get; set; }
        public string AttendanceMarkedBy { get; set; }
        public string AttendanceTime { get; set; }
        public int ClassTotalStrength { get; set; }
        public int ClassPresentStudentCount { get; set; }
        public int ClassAbsentStudentCount { get; set; }
        public int ClassLeaveStudentCount { get; set; }
        public int ClassPrintOrder { get; set; }
        public int SectionPrintOrder { get; set; }
    }
}
