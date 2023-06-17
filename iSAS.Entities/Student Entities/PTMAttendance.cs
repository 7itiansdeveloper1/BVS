

namespace ISas.Entities
{
    public class PTMDatesList
    {
        public string PTMDate { get; set; }
    }
    
    public enum CategoryType
    {
        PTM = 0,
        ORIENTATION = 1
    }
    public class CategoryList
    {
        public string Category{ get; set; }
    }
    public class StudentPTMAttendnaceDetails
    {
        public string Student { get; set; }

        public string Father { get; set; }

        public string Mother { get; set; }
    }
    public class StudentPTMAttendance
    {
        public Student Student { get; set; }
        public StudentPTMAttendnaceDetails StudentPTMAttendanceDetail { get; set; }
    }
    public class ClassPTMAttendance
    {
        public string StudentERPNo { get; set; }

        public string Student { get; set; }

        public string Father { get; set; }

        public string Mother { get; set; }
    }
    
}
