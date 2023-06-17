using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class ExamNameList
    {
        public string ExamId { get; set; }
        public string ExamName { get; set; }
        public int PrintOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class GradingList
    {
        public string GradingId { get; set; }
        public string GradingName { get; set; }
    }
    public class AssessmentNameList
    {
        public string AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public string ExamId { get; set; }
        public string ExamName { get; set; }
        public int PrintOrder { get; set; }
        public bool IsAcademic { get; set; }
        public bool IsNonAcademic { get; set; }
        public bool IsCoScholastic { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class SubjectList
    {
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDisplayName { get; set; }
        public bool IsMainSubject { get; set; }
        public bool IsParentSubject { get; set; }
        public bool IschildSubject { get; set; }
        public bool IsMarkBased { get; set; }
        public bool IsGradeBased { get; set; }
        public bool IsTheorySubject { get; set; }
        public bool IsPracticalSubject { get; set; }
        public bool IsOptionalSubject { get; set; }
        public bool IsCompulsorySubject { get; set; }
        public bool IsAcademicSubject { get; set; }
        public bool IsNonAcademicSubject { get; set; }
        public bool IsCoScholasticSubject { get; set; }
        public string ParentSubjectId { get; set; }
        public string MainSubjectId { get; set; }
        public bool IsWS { get; set; }
        public bool IsCA { get; set; }
        public int PrintOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class ClassList
    {
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        public string ClassFullName { get; set; }
        public int PrintOrder { get; set; }
    }
    public class SectionList
    {
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        public int PrintOrder { get; set; }
    }
    public class StudentsMarkFromDBList
    {
        public bool IsStudentAbsent { get; set; }
        public bool IsStudentonML { get; set; }
        public bool IsStudentExempt { get; set; }
        public bool IsRetestStudent { get; set; }
        public string StudentMark { get; set; }
        public string StudentGrade { get; set; }
    }
    public class StudentsMarkList
    {
        public Student Student { get; set; }
        public StudentsMarkFromDBList StudentsMarkFromDBList { get; set; }
    }
    public class Testing_ClientList
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public int Country { get; set; }
        public string Address { get; set; }
        public bool Married { get; set; }
    }
    public class StudentsMarksFromTableList
    {
        public string StudentERPNo { get; set; }
        public bool IsStudentAbsent { get; set; }
        public bool IsStudentML { get; set; }
        public bool IsStudentExempt { get; set; }
        public string StudentMark { get; set; }
        public string StudentGrade { get; set; }

    }
    public class StudentCoScholasticMarkFromDBList
    {
        //public string StudentERPNo { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
        public string Parameter3 { get; set; }
    }
    public class StudentActivityMarkFromDB
    {
        public string MarksValue { get; set; }
        public string SubjectName { get; set; }
    }
    public class HeaderList
    {
        public string SubjectName { get; set; }
        public string SubjectId { get; set; }
    }
    public class StudentCoScholasticMarkList
    {
        public Student Student { get; set; }
        public StudentCoScholasticMarkFromDBList StudentCoScholsticMarkFromDBList { get; set; }
    }
    public class StudentActivityMarkList
    {
        public StudentActivityMarkList()
        {
            StudentActivityFromDBList = new List<StudentActivityMarkFromDB>();
        }
        public string StudentName { get; set; }
        public int RollNo { get; set; }
        public string AdmNo { get; set; }
        public string ERPNo { get; set; }
        public string Total { get; set; }
        public List<StudentActivityMarkFromDB> StudentActivityFromDBList { get; set; }
    }
    public class StudentCoScholasticMarkFromTableList
    {
        //public string StudentERPNo { get; set; }
        public string SubjectId { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
        public string Parameter3 { get; set; }
    }
    public class StudentActivityMarkFromTableList
    {
        public string StudentERPNo { get; set; }
        public string SubjectId1Mark { get; set; }
        public string SubjectId2Mark { get; set; }
        public string SubjectId3Mark { get; set; }
        public string SubjectId4Mark { get; set; }
        public string SubjectId5Mark { get; set; }
    }
    public class ParentSubjectList
    {
        public string ParentSubjectId { get; set; }
        public string ParentSubjectName { get; set; }
        public int PrintOrder { get; set; }
        public bool IsMarkBased { get; set; }
        public bool IsGradeBased { get; set; }
    }
    public class ClassStudentList
    {
        public bool Select { get; set; }
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
    }
    //public class ClassVerificationList
    //{
    //    public string ERPNo { get; set; }
    //    public string AdmNo { get; set; }
    //    public int RollNo { get; set; }
    //    public string Student { get; set; }
    //    public string SubjectName { get; set; }
    //    public string Assess1Mark { get; set; }
    //    public string Assess2Mark { get; set; }
    //    public string Assess3Mark { get; set; }
    //    public string Assess4Mark { get; set; }
    //    public string Assess5Mark { get; set; }
    //    public string Assess6Mark { get; set; }
    //    public string TotalMark { get; set; }
    //    public string Grade { get; set; }
    //}
    //public class ClassVerificationCoScholasticList
    //{
    //    public string ERPNo { get; set; }
    //    public string AdmNo { get; set; }
    //    public int RollNo { get; set; }
    //    public string Student { get; set; }
    //    public string SubjectName { get; set; }
    //    public string Assess1Mark { get; set; }
    //    public string Assess2Mark { get; set; }
    //    public string Assess3Mark { get; set; }
    //    public string TotalMark { get; set; }
    //    public string Grade { get; set; }
    //}


    //
    public class ActivityMarksEntryModel
    {
        public ActivityMarksEntryModel()
        {
            SessionList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            AssessmentList = new List<SelectListItem>();
            SubjectList = new List<SelectListItem>();
            HeaderList = new List<Entities.HeaderList>();
            StudentsActivityMarkList = new List<StudentActivityMarkList>();
        }

        public string SelectedSessionId { get; set; }
        public string SelectedExamId { get; set; }
        public string SelectedClassId { get; set; }
        public string SelectedSectionId { get; set; }
        public string SelectedAssessmentId { get; set; }
        public string SelectedSubjectId { get; set; }

        public string UserId { get; set; }

        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> AssessmentList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }

        public List<HeaderList> HeaderList { get; set; }
        public List<StudentActivityMarkList> StudentsActivityMarkList { get; set; }
        //public List<StudentCoScholasticMarkFromTableList> StudentCoScholasticMarkFromTableList { get; set; }
    }
    public class Exam_MarksVerificationModel
    {
        public Exam_MarksVerificationModel()
        {
            SessionList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            AssessmentList = new List<SelectListItem>();
            SubjectList = new List<SelectListItem>();
            GradeList = new List<SelectListItem>();

            TitleList = new List<SelectListItem>();
            ValuesList = new List<List<SelectListItem>>();
        }

        public string SelectedSessionId { get; set; }
        public string SelectedExamId { get; set; }
        public string SelectedClassId { get; set; }
        public string SelectedSectionId { get; set; }
        public string SelectedAssessmentId { get; set; }
        public string SelectedSubjectId { get; set; }
        public string SelectedGradeId { get; set; }
        public string SubjectCategory { get; set; }

        public string SelectedTermName { get; set; }
        public string SelectedClassName { get; set; }
        public string SelectedSectionName { get; set; }
        public string SelectedSubjectName { get; set; }


        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> AssessmentList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> GradeList { get; set; }

        public List<SelectListItem> TitleList { get; set; }
        public List<List<SelectListItem>> ValuesList { get; set; }

        //public List<StudentsMarkList> StudentsMarkList { get; set; }
        //public List<ClassVerificationList> ClassVerificationList { get; set; }
    }
    public class Exam_MarksVerificationCoScholasticModels
    {
        public Exam_MarksVerificationCoScholasticModels()
        {
            SessionList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            AssessmentList = new List<SelectListItem>();
            SubjectList = new List<SelectListItem>();
            GradeList = new List<SelectListItem>();
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
        }

        public string SelectedSessionId { get; set; }
        public string SelectedExamId { get; set; }
        public string SelectedClassId { get; set; }
        public string SelectedSectionId { get; set; }
        public string SelectedAssessmentId { get; set; }
        public string SelectedSubjectId { get; set; }
        public string SelectedGradeId { get; set; }
        public string SubjectCategory { get; set; }

        public string SelectedTermName { get; set; }
        public string SelectedClassName { get; set; }
        public string SelectedSectionName { get; set; }
        public string SelectedSubjectName { get; set; }

        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> AssessmentList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> GradeList { get; set; }

        public List<string> HeaderNameList { get; set; }
        public List<List<string>> ValueList { get; set; }

        //public List<StudentsMarkList> StudentsMarkList { get; set; }
        //public List<ClassVerificationList> ClassVerificationList { get; set; }
        //public List<ClassVerificationCoScholasticList> ClassVerificationCoScholasticList { get; set; }
    }
}
