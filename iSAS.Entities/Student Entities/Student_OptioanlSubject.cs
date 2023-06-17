
namespace ISas.Entities
{
    public class Student_OptionalSubject_CRUD
    {
        public string StudentERPNo { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
        public string Parameter3 { get; set; }
    }
    public class OptionalSubjectParametersList
    {
        public string FieldName { get; set; }
        public string FieldDisplayName { get; set; }
    }
    public class StudentList
    {
        public Student Student { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
        public string Parameter3 { get; set; }
    }
    public class OptionalSubjectList
    {
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int PrintOrder { get; set; }
    }
}
