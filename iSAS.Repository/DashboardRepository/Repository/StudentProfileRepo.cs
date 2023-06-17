using ISas.Entities.DashboardEntities;
using ISas.Entities.Student_Entities;
using ISas.Repository.DashboardRepository.IRepository;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class StudentProfileRepo : IStudentProfileRepo
    {
        public StudentProfile GetStudentProfileById(string Stud_UID)
        {
            StudentProfile studentProfileDetails = new StudentProfile();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from GetStudentProfie_NEW ('" + Stud_UID + "')", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                studentProfileDetails = ds.Tables[0].AsEnumerable().Select(r => new StudentProfile
                {
                    Address = r.Field<string>("Address"),
                    AdmNo = r.Field<string>("AdmNo"),
                    SRNo = r.Field<string>("SRNo"),
                    AlternateNumber = r.Field<string>("AlternateNumber"),
                    Class = r.Field<string>("Class"),
                    DOA = r.Field<string>("DOA"),
                    DOB = r.Field<string>("DOB"),
                    DriverName = r.Field<string>("DriverName"),
                    DropTime = r.Field<string>("DropTime"),
                    ERP = r.Field<string>("ERP"),
                    Father = r.Field<string>("Father"),
                    FMobileNo = r.Field<string>("FMobileNo"),
                    Gender = r.Field<string>("Gender"),
                    HelperName = r.Field<string>("HelperName"),
                    MMobileNo = r.Field<string>("MMobileNo"),
                    Mother = r.Field<string>("Mother"),
                    Pickuptime = r.Field<string>("Pickuptime"),
                    RollNo = r.Field<int>("RollNo"),
                    RouteName = r.Field<string>("RouteName"),
                    SMSF = r.Field<bool>("SMSF"),
                    SMSG = r.Field<bool>("SMSG"),
                    SMSM = r.Field<bool>("SMSM"),
                    SMSNo = r.Field<string>("SMSNo"),
                    SMSO = r.Field<bool>("SMSO"),
                    StopName = r.Field<string>("StopName"),
                    Student = r.Field<string>("Student"),
                    Status = r.Field<string>("Status"),
                    Contact1 = r.Field<string>("Contact1"),
                    Contact2 = r.Field<string>("Contact2"),
                    Address1 = r.Field<string>("Address1"),

                }).FirstOrDefault();

                if (studentProfileDetails != null)
                    studentProfileDetails.StudentImages = GetStudentImages(Stud_UID, "ALL");
            }
            return studentProfileDetails;
        }


        public Student_ImagesModels GetStudentImages(string Stud_UID, string type)
        {
            Student_ImagesModels model = new Student_ImagesModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from GetStudentProfie_NEW ('" + Stud_UID + "')", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (type == "ALL")
                {
                    model = ds.Tables[0].AsEnumerable().Select(r => new Student_ImagesModels
                    {
                        ERPNo = r.Field<string>("ERP"),
                        FatherImageURL = r.Field<string>("Father_Image"),
                        GrandFatherImageURL = r.Field<string>("Grand_FatherImage"),
                        GrandMotherImageURL = r.Field<string>("Grand_MotherImage"),
                        GuardianImageURL = r.Field<string>("Gardian_image"),
                        MotherImageURL = r.Field<string>("Mother_Image"),
                        OtherImageURL = r.Field<string>("PickedUPBy_Image"),
                        StudentImageURL = r.Field<string>("Stud_Image"),
                    }).FirstOrDefault();
                }else
                {
                    string fieldName = type == "P" ? "PickedUPBy_Image" : type == "F" ? "Father_Image"
                        : type == "M" ? "Mother_Image" : type == "G" ? "Gardian_image" : type == "GF" ? "Grand_FatherImage"
                        : type == "GM" ? "Grand_MotherImage" : "Stud_Image";
                    model = ds.Tables[0].AsEnumerable().Select(r => new Student_ImagesModels
                    {
                        StudentImageURL = r.Field<string>(fieldName),
                        ERPNo = r.Field<string>("ERP"),
                    }).FirstOrDefault();
                    model.UploadFor = type;

                    model.ImageForName = type == "P" ? "Picked up by Image" : type == "F" ? "Father Image"
                        : type == "M" ? "Mother Image" : type == "G" ? "Gardian Image" : type == "GF" ? "Grand Father Image"
                        : type == "GM" ? "Grand Mother Image" : "Student Image";
                }
            }
            return model;
        }
    }
}
