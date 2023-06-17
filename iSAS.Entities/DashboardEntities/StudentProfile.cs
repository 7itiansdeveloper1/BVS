using ISas.Entities.Student_Entities;
using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.DashboardEntities
{
    public class StudentProfile
    {
        public StudentProfile()
        {
            StudentImages = new Student_ImagesModels();
        }

        [Display(Name = "ERP No")]
        public string ERP { get; set; }

        [Display(Name = "Date of Admission")]
        public string DOA { get; set; }

        public string SRNo { get; set; }

        [Display(Name = "Admission No")]
        public string AdmNo { get; set; }

        [Display(Name = "Roll No")]
        public int RollNo { get; set; }

        [Display(Name = "Student Name")]
        public string Student { get; set; }

        [Display(Name = "Class")]
        public string Class { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Display(Name = "Father's Name")]
        public string Father { get; set; }

        [Display(Name = "Mother's Name")]
        public string Mother { get; set; }

        [Display(Name = "Address 1")]
        public string Address { get; set; }

        [Display(Name = "Father Mobile No")]
        public string FMobileNo { get; set; }

        [Display(Name = "Mother Mobile No")]
        public string MMobileNo { get; set; }

        [Display(Name = "SMS No")]
        public string SMSNo { get; set; }

        [Display(Name = "Alternate Number")]
        public string AlternateNumber { get; set; }

        [Display(Name = "Route Name")]
        public string RouteName { get; set; }

        [Display(Name = "Stop Name")]
        public string StopName { get; set; }

        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }

        [Display(Name = "Helper Name")]
        public string HelperName { get; set; }

        [Display(Name = "Pick-Up Time")]
        public string Pickuptime { get; set; }

        [Display(Name = "Drop Time")]
        public string DropTime { get; set; }

        [Display(Name = "SMS to Father")]
        public bool SMSF { get; set; }

        [Display(Name = "SMS to Mother")]
        public bool SMSM { get; set; }

        [Display(Name = "SMS to Mother")]
        public bool SMSG { get; set; }

        [Display(Name = "SMS to Other")]
        public bool SMSO { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Contact1")]
        public string Contact1 { get; set; }

        [Display(Name = "Contact2")]
        public string Contact2 { get; set; }

        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        public Student_ImagesModels StudentImages { get; set; }
    }
}
