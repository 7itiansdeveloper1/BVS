using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using ISas.Repository.Interface;
using ISas.Repository.Implementation;

namespace ISas.Web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<ILoginData, LoginData>();
            container.RegisterType<IStudentClass, StudentClassRepositroy>();
            container.RegisterType<IStudentSection, StudentSectionRepository>();
            container.RegisterType<IStudentSession, StudentSessionRepository>();
            container.RegisterType<IStudentAttendance, StudentAttendanceRepository>();
            container.RegisterType<IExam, ExamRepository>();
            container.RegisterType<IPtmAttendance, PtmAttendanceRepository>();
            container.RegisterType<IAttendanceRegister, AttendanceRegisterRepository>();
            container.RegisterType<IStudentUpdation, StudentUpdationRepository>();
            container.RegisterType<IStudent_OptionalSubject, Student_OptionalSubjectRepository>();
            container.RegisterType<IExam_AchievementRepository, Exam_AchievementRepository>();
            container.RegisterType<IExam_RemarkRepository, Repository.ImplementationGetExamNameList.Exam_RemarkRepository>();
            container.RegisterType<IExam_ReportCard, Exam_ReportCardRepository>();
            container.RegisterType<IExam_Result, Exam_ResultRepo>();
            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<IStateRepository, StateRepository>();
            container.RegisterType<ICityRepository, CityRepository>();
            container.RegisterType<IAreaRepository, AreaRepository>();
            container.RegisterType<ISchool_DocumentMasterRepo, School_DocumentMasterRepo>();
            container.RegisterType<Repository.StudentRegistrationRepository.IRepository.IStudentRegistrationRepo, Repository.StudentRegistrationRepository.Repository.StudentRegistrationRepo>();
            container.RegisterType<ISchoolAdmissionCategoryRepo, SchoolAdmissionCategoryRepo>();
            container.RegisterType<Repository.StudentRegistrationRepository.IRepository.IStudentAdmissionRepo, Repository.StudentRegistrationRepository.Repository.StudentAdmissionRepo>();
            container.RegisterType<Repository.StudentRegistrationRepository.IRepository.IRouteStopRepo, Repository.StudentRegistrationRepository.Repository.RouteStopRepo>();
            container.RegisterType<Repository.StudentRepository.IRepository.IMyClassRepo, Repository.StudentRepository.Repository.MyClassRepo>();

            container.RegisterType<IStudentTC, StudentTCRepositroy>();
            container.RegisterType<ICommon_AttachmentRefRepo, Common_AttachmentRefRepo>();

            container.RegisterType<Repository.StaffRepository.IRepository.IStaff_StaffDetailMasterRepo, Repository.StaffRepository.Repository.Staff_StaffDetailMasterRepo>();
            container.RegisterType<Repository.StaffRepository.IRepository.IStaff_AttendanceRegisterRepo, Repository.StaffRepository.Repository.Staff_AttendanceRegisterRepo>();
            container.RegisterType<Repository.StaffRepository.IRepository.IStaff_DocumentUploadRepo, Repository.StaffRepository.Repository.Staff_DocumentUploadRepo>();


            container.RegisterType<IStudent_NSORepo, Student_NSORepo>();
            container.RegisterType<Repository.SMSManagement.IRepository.ISMSManagementRepo, Repository.SMSManagement.Repository.SMSManagementRepo>();
            container.RegisterType<Repository.SMSManagement.IRepository.ISMSTempleteRepo, Repository.SMSManagement.Repository.SMSTempleteRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IStudentProfileRepo, Repository.DashboardRepository.Repository.StudentProfileRepo>();
            container.RegisterType<Repository.StudentRepository.IRepository.IStudent_IdentityCardRepo, Repository.StudentRepository.Repository.Student_IdentityCardRepo>();

            container.RegisterType<Repository.DashboardRepository.IRepository.ICommon_NECNRepo, Repository.DashboardRepository.Repository.Common_NECNRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IDashboardRepo, Repository.DashboardRepository.Repository.DashboardRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IDashBoard_ParentRequestMasterRepo, Repository.DashboardRepository.Repository.DashBoard_ParentRequestMasterRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IStudentFeeDetailsRepo, Repository.DashboardRepository.Repository.StudentFeeDetailsRepo>();
            container.RegisterType<Controllers.Dashboard.ParentRequestMasterController, Controllers.Dashboard.ParentRequestMasterController>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IDashBoard_StudentStataticsRepo, Repository.DashboardRepository.Repository.DashBoard_StudentStataticsRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IToDo_TaskRepo, Repository.DashboardRepository.Repository.ToDo_TaskRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IDashBoard_StaffStataticsRepo, Repository.DashboardRepository.Repository.DashBoard_StaffStataticsRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IStudentHomeworkRepo, Repository.DashboardRepository.Repository.StudentHomeworkRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IOnlineClass_StaffRepo, Repository.DashboardRepository.Repository.OnlineClass_StaffRepo>();
            container.RegisterType<Repository.DashboardRepository.IRepository.IOnlineClass_StudentRepo, Repository.DashboardRepository.Repository.OnlineClass_StudentRepo>();
            

            container.RegisterType<ICommonRepo, CommonRepo>();
            container.RegisterType<IMarksEntry_StudentWiseRepo, MarksEntry_StudentWiseRepo>();
            container.RegisterType<Repository.TransportRepo.IRepository.IAvailTransportRepo, Repository.TransportRepo.Repository.AvailTransportRepo>();

            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_TransactionRepo, Repository.FeeModuleRepo.Repository.Fee_TransactionRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_ConcessionRepo, Repository.FeeModuleRepo.Repository.Fee_ConcessionRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_MiscDuesRepo, Repository.FeeModuleRepo.Repository.Fee_MiscDuesRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_InvoiceCreationRepo, Repository.FeeModuleRepo.Repository.Fee_InvoiceCreationRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_FeeHeadMasterRepo, Repository.FeeModuleRepo.Repository.Fee_FeeHeadMasterRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_FeeStructureMasterRepo, Repository.FeeModuleRepo.Repository.Fee_FeeStructureMasterRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_InstallmentSetupRepo, Repository.FeeModuleRepo.Repository.Fee_InstallmentSetupRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_FinePolicyRepo, Repository.FeeModuleRepo.Repository.Fee_FinePolicyRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_DueSetupRepo, Repository.FeeModuleRepo.Repository.Fee_DueSetupRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_ConcessionPolicyRepo, Repository.FeeModuleRepo.Repository.Fee_ConcessionPolicyRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_ReceiptCancellationRepo, Repository.FeeModuleRepo.Repository.Fee_ReceiptCancellationRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_DefaulterDetailsRepo, Repository.FeeModuleRepo.Repository.Fee_DefaulterDetailsRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_ReceiptHeaderMasterRepo, Repository.FeeModuleRepo.Repository.Fee_ReceiptHeaderMasterRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_FeeBillReportRepo, Repository.FeeModuleRepo.Repository.Fee_FeeBillReportRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_ReportRepo, Repository.FeeModuleRepo.Repository.Fee_ReportRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_PaymentGatewayMasterRepo, Repository.FeeModuleRepo.Repository.Fee_PaymentGatewayMasterRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_OnlineSettlementRepo, Repository.FeeModuleRepo.Repository.Fee_OnlineSettlementRepo>();
            container.RegisterType<Repository.FeeModuleRepo.IRepository.IFee_OnlineTransactionRepo, Repository.FeeModuleRepo.Repository.Fee_OnlineTransactionRepo>();



            container.RegisterType<Repository.Academic.IRepository.IAcademic_DocumentMasterRepo, Repository.Academic.Repository.Academic_DocumentMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_DocumentMappingRepo, Repository.Academic.Repository.Academic_DocumentMappingRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_SectionMasterRepo, Repository.Academic.Repository.Academic_SectionMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_SchoolSetupRepo, Repository.Academic.Repository.Academic_SchoolSetupRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_WingSetupRepo, Repository.Academic.Repository.Academic_WingSetupRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_ClassSetupRepo, Repository.Academic.Repository.Academic_ClassSetupRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_SectionSetupRepo, Repository.Academic.Repository.Academic_SectionSetupRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_HouseMasterRepo, Repository.Academic.Repository.Academic_HouseMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_ProfessionMasterRepo, Repository.Academic.Repository.Academic_ProfessionMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_QualificationMasterRepo, Repository.Academic.Repository.Academic_QualificationMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_DesignationMasterRepo, Repository.Academic.Repository.Academic_DesignationMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_DepartmentMasterRepo, Repository.Academic.Repository.Academic_DepartmentMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_BankMasterRepo, Repository.Academic.Repository.Academic_BankMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_HolidayMasterRepo, Repository.Academic.Repository.Academic_HolidayMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_HolidayDeclarationRepo, Repository.Academic.Repository.Academic_HolidayDeclarationRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_HolidayAllocationRepo, Repository.Academic.Repository.Academic_HolidayAllocationRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_DynamicReportWizardRepo, Repository.Academic.Repository.Academic_DynamicReportWizardRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_PTMOpendDayRepo, Repository.Academic.Repository.Academic_PTMOpendDayRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_DirectoryMasterRepo, Repository.Academic.Repository.Academic_DirectoryMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_HomeWorkMasterRepo, Repository.Academic.Repository.Academic_HomeWorkMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_SyllabusMasterRepo, Repository.Academic.Repository.Academic_SyllabusMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_SessionMasterRepo, Repository.Academic.Repository.Academic_SessionMasterRepo>();
            container.RegisterType<Repository.Academic.IRepository.IAcademic_FODRepo, Repository.Academic.Repository.Academic_FODRepo>();

            container.RegisterType<Repository.Academic.IRepository.IAcademic_ERPBill_Repo, Repository.Academic.Repository.Academic_ERPBill_Repo>();
            container.RegisterType<Repository.TransportRepo.IRepository.ITransport_RouteMasterRepo, Repository.TransportRepo.Repository.Transport_RouteMasterRepo>();
            container.RegisterType<Repository.TransportRepo.IRepository.ITransport_StopSetupRepo, Repository.TransportRepo.Repository.Transport_StopSetupRepo>();
            container.RegisterType<Repository.TransportRepo.IRepository.ITransport_VehicleSetupRepo, Repository.TransportRepo.Repository.Transport_VehicleSetupRepo>();

            container.RegisterType<IExam_StudentProfileRepo, Exam_StudentProfileRepo>();

            container.RegisterType<Repository.Library.IRepository.ILibrary_BookTitleMasterRepo, Repository.Library.Repository.Library_BookTitleMasterRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_SetupRepo, Repository.Library.Repository.Library_SetupRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_AuthorMasterRepo, Repository.Library.Repository.Library_AuthorMasterRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_SupplierMasterRepo, Repository.Library.Repository.Library_SupplierMasterRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_PublisherMasterRepo, Repository.Library.Repository.Library_PublisherMasterRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_SubjectMasterRepo, Repository.Library.Repository.Library_SubjectMasterRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_BookMasterRepo, Repository.Library.Repository.Library_BookMasterRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_FineSetupRepo, Repository.Library.Repository.Library_FineSetupRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_GenerateBarcodeRepo, Repository.Library.Repository.Library_GenerateBarcodeRepo>();
            container.RegisterType<Repository.Library.IRepository.ILibrary_ReportRepo, Repository.Library.Repository.Library_ReportRepo>();
            container.RegisterType<Repository.Library.IRepository.IeLibraryRepo, Repository.Library.Repository.eLibraryRepo>();


            container.RegisterType<Repository.StudentRegistrationRepository.IRepository.IStudent_ReportRepo, ISas.Repository.StudentRegistrationRepository.Repository.Student_ReportRepo>();
            container.RegisterType<Repository.StudentRepository.IRepository.IStudent_CertificateRepo, Repository.StudentRepository.Repository.Student_CertificateRepo>();
            container.RegisterType<IExam_MarksVerificationRepo, Exam_MarksVerificationRepo>();
            container.RegisterType<IStudent_AttendanceReportRepo, Student_AttendanceReportRepo>();

            container.RegisterType<Repository.StaffRepository.IRepository.IStaff_ReportsRepo, Repository.StaffRepository.Repository.Staff_ReportsRepo>();

            container.RegisterType<Repository.SMSManagement.IRepository.ISMS_ReportRepo, Repository.SMSManagement.Repository.SMS_ReportRepo>();
            

            container.RegisterType<Repository.Library.IRepository.ILibrary_TransactionRepo, Repository.Library.Repository.Library_TransactionRepo>();

            container.RegisterType<Repository.StaffRepository.IRepository.IStaff_AttendanceReportsRepo, Repository.StaffRepository.Repository.Staff_AttendanceReportsRepo>();
            container.RegisterType<Repository.TransportRepo.IRepository.ITransport_ReportRepo, Repository.TransportRepo.Repository.Transport_ReportRepo>();

            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_LeaveMasterRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_LeaveMasterRepo>();
            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_LeaveRegisterRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_LeaveRegisterRepo>();
            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_SalaryHeadRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_SalaryHeadRepo>();
            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_PayBandMasterRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_PayBandMasterRepo>();
            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_CTC_SetupRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_CTC_SetupRepo>();
            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_StaffEnrollmentRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_StaffEnrollmentRepo>();
            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_FinalCTSRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_FinalCTSRepo >();
            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_AttendanceProcessRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_AttendanceProcessRepo>();
            container.RegisterType<Repository.HR_PayrollRepo.IRepository.IHR_Payroll_SalaryRegisterRepo, Repository.HR_PayrollRepo.Repository.HR_Payroll_SalaryRegisterRepo>();

            container.RegisterType<Repository.UserRepo.IRepository.IUserCreationRepo, Repository.UserRepo.Repository.UserCreationRepo>();
            container.RegisterType<Repository.UserRepo.IRepository.IUserPermissionRepo, Repository.UserRepo.Repository.UserPermissionRepo>();
            container.RegisterType<Repository.UserRepo.IRepository.IStudentPermissionRepo, Repository.UserRepo.Repository.StudentPermissionRepo>();
            container.RegisterType<Repository.StudentRegistrationRepository.IRepository.ISiblingAllotmentRepo, Repository.StudentRegistrationRepository.Repository.SiblingAllotmentRepo>();

            container.RegisterType<Repository.TimeTable_Repo.IRepository.ITimeTable_PeriodMatrixRepo, Repository.TimeTable_Repo.Repository.TimeTable_PeriodMatrixRepo>();
            container.RegisterType<Repository.TimeTable_Repo.IRepository.ITimeTable_StaffWorkLoadSetupRepo, Repository.TimeTable_Repo.Repository.TimeTable_StaffWorkLoadSetupRepo>();
            container.RegisterType<Repository.TimeTable_Repo.IRepository.ITimeTable_SetupRepo, Repository.TimeTable_Repo.Repository.TimeTable_SetupRepo>();
            container.RegisterType<Repository.TimeTable_Repo.IRepository.ITimeTable_PeriodTimingSetupRepo, Repository.TimeTable_Repo.Repository.TimeTable_PeriodTimingSetupRepo>();
            container.RegisterType<Repository.TimeTable_Repo.IRepository.ITimeTable_AdjustmentRepo, Repository.TimeTable_Repo.Repository.TimeTable_AdjustmentRepo>();
            container.RegisterType<Repository.TimeTable_Repo.IRepository.ITimeTable_SubjectMasterRepo, Repository.TimeTable_Repo.Repository.TimeTable_SubjectMasterRepo>();

            container.RegisterType<Repository.ExaminationRepository.IRepository.IExam_TemplateSetupRepo, Repository.ExaminationRepository.Repository.Exam_TemplateSetupRepo>();
            container.RegisterType<Repository.ExaminationRepository.IRepository.IExamination_SubjectMasterRepo, Repository.ExaminationRepository.Repository.Examination_SubjectMasterRepo>();
            container.RegisterType<Repository.ExaminationRepository.IRepository.IExamination_AssessmentSetupRepo, Repository.ExaminationRepository.Repository.Examination_AssessmentSetupRepo>();
            container.RegisterType<Repository.ExaminationRepository.IRepository.IExamination_ChildSubjectSetupRepo, Repository.ExaminationRepository.Repository.Examination_ChildSubjectSetupRepo>();
            container.RegisterType<Repository.ExaminationRepository.IRepository.IExamination_MarksEntryRepo, Repository.ExaminationRepository.Repository.Examination_MarksEntryRepo>();
            container.RegisterType<Repository.ExaminationRepository.IRepository.IExamination_RemarksCenterRepo, Repository.ExaminationRepository.Repository.Examination_RemarksCenterRepo>();
            container.RegisterType<Repository.ExaminationRepository.IRepository.IExamination_RemarksEntryRepo, Repository.ExaminationRepository.Repository.Examination_RemarksEntryRepo>();
            container.RegisterType<Repository.ExaminationRepository.IRepository.IExamination_ProfileEntryRepo, Repository.ExaminationRepository.Repository.Examination_ProfileEntryRepo>();
            container.RegisterType<Repository.ExaminationRepository.IRepository.IExamination_ReportCardRepo, Repository.ExaminationRepository.Repository.Examination_ReportCardRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExam_TargetListRepo, Repository.ExamRepository.Repository.Exam_TargetListRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExam_ReportsRepo, Repository.ExamRepository.Repository.Exam_ReportsRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExam_QuestionPaperRepo, Repository.ExamRepository.Repository.Exam_QuestionPaperRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExam_QuestionMasterRepo, Repository.ExamRepository.Repository.Exam_QuestionMasterRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExam_OnlineAssessmentRepo, Repository.ExamRepository.Repository.Exam_OnlineAssessmentRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExam_AnswersheetRepo, Repository.ExamRepository.Repository.Exam_AnswersheetRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExamReportcardTemplateRepo, Repository.ExamRepository.Repository.ExamReportcardTemplateRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExam_LockingRepo, Repository.ExamRepository.Repository.Exam_LockingRepo>();
            container.RegisterType<Repository.ExamRepository.IRepository.IExam_ClassSubjectSetupRepo, Repository.ExamRepository.Repository.Exam_ClassSubjectSetupRepo>();
            return container;
        }
    }
}