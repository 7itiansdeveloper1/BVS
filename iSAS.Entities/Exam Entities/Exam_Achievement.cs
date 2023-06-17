
namespace ISas.Entities
{
    public class StudentAchievementTable
    {
        public string StudentERPNo { get; set; }
        public string Achievement { get; set; }
        
    }
    //public class OptionalSubjectParametersList
    //{
    //    public string FieldName { get; set; }
    //    public string FieldDisplayName { get; set; }
    //}
    public class StudentAchievementList
    {
        public Student Student { get; set; }
        public string Achievement { get; set; }
        
    }
    //public class OptionalSubjectList
    //{
    //    public string SubjectId { get; set; }
    //    public string SubjectName { get; set; }
    //    public int PrintOrder { get; set; }
    //}
}
