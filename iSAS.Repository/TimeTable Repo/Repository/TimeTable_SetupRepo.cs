using ISas.Entities.TimeTable_Entities;
using ISas.Repository.TimeTable_Repo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.TimeTable_Repo.Repository
{
    public class TimeTable_SetupRepo : ITimeTable_SetupRepo
    {
        public List<TimeTable_ConfigurationModels> GetLandingPageDetails()
        {
            List<TimeTable_ConfigurationModels> configurationList = new List<TimeTable_ConfigurationModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_TimeTableSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                configurationList = ds.Tables[0].AsEnumerable().Select(r => new TimeTable_ConfigurationModels
                {
                    ClassName = r.Field<string>("ClassName"),
                    ClassSectionId = r.Field<string>("ClassSectionId"),
                    ClassTeacher = r.Field<string>("ClassTeacher"),
                    Matrix = r.Field<string>("Matrix"),
                }).ToList();
            }
            return configurationList;
        }


        public TimeTable_SetupModels TimeTable_Setup_FormLoad(string ClassSectionId)
        {
            TimeTable_SetupModels model = new TimeTable_SetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_ClassTimeTable_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                model.SubjectList = ds.Tables[0].AsEnumerable().Select(r => new TimeTableSubjectListModel
                {
                    Text = r.Field<string>("StaffName"),
                    Value = r.Field<string>("SubjectStaffId"),
                    StaffId = r.Field<string>("StaffId"),
                    StaffName = r.Field<string>("StaffFullName"),
                }).ToList();

                //model.TeacherList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                //{
                //    Text = r.Field<string>("StaffName"),
                //    Value = r.Field<string>("StaffId"),
                //}).ToList();

                //model.DaysList = new List<string>();
                //model.PeriodDetailsList = new List<TimeTable_PeriodModel>();

                if (ds.Tables[1].Columns.Count > 1)
                {
                    for (int i = 1; i < ds.Tables[1].Columns.Count; i++)
                    {
                        model.DaysList.Add(ds.Tables[1].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        TimeTable_PeriodModel periodDetails = new TimeTable_PeriodModel();
                        string TempPeriod = ds.Tables[1].Rows[i][0].ToString();
                        List<string> periodStr = TempPeriod.Split('_').ToList(); ;
                        if (periodStr.Count == 2)
                        {
                            periodDetails.PeriodName = periodStr[0];
                            periodDetails.PeriodTime = periodStr[1];
                        }
                        List<TimeTable_InfoModel> tempPeriodInfoList = new List<TimeTable_InfoModel>();
                        for (int j = 1; j < ds.Tables[1].Columns.Count; j++)
                        {
                            string currentStr = ds.Tables[1].Rows[i][j].ToString();
                            TimeTable_InfoModel newRowInfo = new TimeTable_InfoModel();
                            if (!string.IsNullOrEmpty(currentStr))
                            {
                                //newRowInfo.TeacherId = currentStr.Substring(0, 20).Substring(20 - 10);
                                List<string> strList = currentStr.Split('@').ToList();
                                if (strList.Count == 2)
                                {
                                    newRowInfo.SubjectName = strList[0].Replace("#", " || ");
                                    newRowInfo.SubjectId = strList[1];
                                    //newRowInfo.TeacherName = strList[2];
                                }
                            }
                            tempPeriodInfoList.Add(newRowInfo);
                        }

                        periodDetails.PeriodInfoList.AddRange(tempPeriodInfoList);
                        model.PeriodDetailsList.Add(periodDetails);
                    }
                }
            }
            return model;
        }


        public TimeTable_SetupModels getSubjects(string p, string d, string ClassSectionId, string classsection, string classteacher)
        {
            TimeTable_SetupModels model = new TimeTable_SetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_SubjectList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                model.SubjectList = ds.Tables[0].AsEnumerable().Select(r => new TimeTableSubjectListModel
                {
                    Text = r.Field<string>("StaffName"),
                    Value = r.Field<string>("SubjectStaffId"),
                    StaffId = r.Field<string>("StaffId"),
                    StaffName = r.Field<string>("StaffFullName"),
                }).ToList();
            }
            model.p = p;
            model.d = d;
            model.ClassSectionId = ClassSectionId;
            model.ClassSectionName = classsection;
            model.ClassTeacherName = classteacher;
            return model;
        }

        public TimeTable_SetupModels TimeTable_Setup_FormLoad_V1(string ClassSectionId)
        {
            TimeTable_SetupModels model = new TimeTable_SetupModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_ClassTimeTable_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.SubjectList = ds.Tables[0].AsEnumerable().Select(r => new TimeTableSubjectListModel
                {
                    Value = r.Field<string>("SubjectStaffId"),
                    Text = r.Field<string>("StaffName"),

                }).ToList();



                if (ds.Tables[1].Columns.Count > 1)
                {
                    //for (int i = 1; i < ds.Tables[1].Columns.Count; i++)
                    //{
                    //    model.DaysList.Add(ds.Tables[1].Columns[i].ColumnName);
                    //}

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {

                        SinglePeriodTimeTable singlePeriod = new SinglePeriodTimeTable();
                        string TempPeriod = ds.Tables[1].Rows[i][0].ToString();
                        List<string> periodStr = TempPeriod.Split('_').ToList(); ;
                        if (periodStr.Count == 2)
                        {
                            singlePeriod.PeriodName = periodStr[0];
                            singlePeriod.PeriodTime = periodStr[1];
                        }
                        List<singledropDown> singledropsList = new List<singledropDown>();
                        for (int j = 1; j < ds.Tables[1].Columns.Count; j++)
                        {
                            string currentStr = ds.Tables[1].Rows[i][j].ToString();
                            //List<SelectListItem> d1 = new List<SelectListItem>();
                            singledropDown singledrops = new singledropDown();
                            //List<SelectListItem> dropdown = new List<SelectListItem>();
                            singledrops.dropdown = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                            {
                                Value = r.Field<string>("SubjectStaffId"),
                                Text = r.Field<string>("StaffName"),
                                Selected = (r.Field<string>("SubjectStaffId") == currentStr)

                            }).ToList();
                            singledrops.teacherSubjectCode = currentStr;
                            singledropsList.Add(singledrops);
                        }
                        //periodDetails.PeriodInfoList_V1.AddRange(tempPeriodInfoList);

                        singlePeriod.dropdownList.AddRange(singledropsList);
                        model.singleperiodtimetable.Add(singlePeriod);
                    }
                }



                if (ds.Tables[2].Columns.Count > 1)
                {
                    for (int i = 1; i < ds.Tables[2].Columns.Count; i++)
                    {
                        model.DaysList.Add(ds.Tables[2].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        TimeTable_PeriodModel periodDetails = new TimeTable_PeriodModel();
                        string TempPeriod = ds.Tables[2].Rows[i][0].ToString();
                        List<string> periodStr = TempPeriod.Split('_').ToList(); ;
                        if (periodStr.Count == 2)
                        {
                            periodDetails.PeriodName = periodStr[0];
                            periodDetails.PeriodTime = periodStr[1];
                        }
                        List<TimeTable_InfoModel> tempPeriodInfoList = new List<TimeTable_InfoModel>();
                        for (int j = 1; j < ds.Tables[2].Columns.Count; j++)
                        {
                            string currentStr = ds.Tables[2].Rows[i][j].ToString();
                            TimeTable_InfoModel newRowInfo = new TimeTable_InfoModel();
                            if (!string.IsNullOrEmpty(currentStr))
                            {
                                //newRowInfo.TeacherId = currentStr.Substring(0, 20).Substring(20 - 10);
                                List<string> strList = currentStr.Split('@').ToList();
                                if (strList.Count == 2)
                                {
                                    newRowInfo.SubjectName = strList[0].Replace("#", " || ");
                                    newRowInfo.SubjectId = strList[1];
                                    //newRowInfo.TeacherName = strList[2];
                                }
                            }
                            tempPeriodInfoList.Add(newRowInfo);
                        }

                        periodDetails.PeriodInfoList.AddRange(tempPeriodInfoList);
                        model.PeriodDetailsList.Add(periodDetails);
                    }
                }

            }
            return model;
        }




        public List<SelectListItem> GetTeacherListBySubjectId(string ClassSectionId, string SubjectId)
        {
            List<SelectListItem> teacherList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_ClassTimeTable_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                teacherList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StaffName"),
                    Value = r.Field<string>("StaffId"),
                }).ToList();
            }
            return teacherList;
        }

        public Tuple<int, string> TimeTable_Setup_CRUD_V1(TimeTable_SetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string monMatrix = ""; string tueMatrix = ""; string wedMatrix = "";
                string thuMatrix = ""; string friMatrix = ""; string satMatrix = "";

                for (int i = 0; i < model.singleperiodtimetable.Count; i++)
                {
                    if (model.singleperiodtimetable[i].dropdownList.Count > 0 && !string.IsNullOrEmpty(model.singleperiodtimetable[i].dropdownList[0].teacherSubjectCode))
                        monMatrix += model.singleperiodtimetable[i].PeriodName + "_" + model.singleperiodtimetable[i].dropdownList[0].teacherSubjectCode + ",";

                    if (model.singleperiodtimetable[i].dropdownList.Count > 1 && !string.IsNullOrEmpty(model.singleperiodtimetable[i].dropdownList[1].teacherSubjectCode))
                        tueMatrix += model.singleperiodtimetable[i].PeriodName + "_" + model.singleperiodtimetable[i].dropdownList[1].teacherSubjectCode + ",";

                    if (model.singleperiodtimetable[i].dropdownList.Count > 2 && !string.IsNullOrEmpty(model.singleperiodtimetable[i].dropdownList[2].teacherSubjectCode))
                        wedMatrix += model.singleperiodtimetable[i].PeriodName + "_" + model.singleperiodtimetable[i].dropdownList[2].teacherSubjectCode + ",";

                    if (model.singleperiodtimetable[i].dropdownList.Count > 3 && !string.IsNullOrEmpty(model.singleperiodtimetable[i].dropdownList[3].teacherSubjectCode))
                        thuMatrix += model.singleperiodtimetable[i].PeriodName + "_" + model.singleperiodtimetable[i].dropdownList[3].teacherSubjectCode + ",";

                    if (model.singleperiodtimetable[i].dropdownList.Count > 4 && !string.IsNullOrEmpty(model.singleperiodtimetable[i].dropdownList[4].teacherSubjectCode))
                        friMatrix += model.singleperiodtimetable[i].PeriodName + "_" + model.singleperiodtimetable[i].dropdownList[4].teacherSubjectCode + ",";

                    if (model.singleperiodtimetable[i].dropdownList.Count > 5 && !string.IsNullOrEmpty(model.singleperiodtimetable[i].dropdownList[5].teacherSubjectCode))
                        satMatrix += model.singleperiodtimetable[i].PeriodName + "_" + model.singleperiodtimetable[i].dropdownList[5].teacherSubjectCode + ",";
                }

                if (!string.IsNullOrEmpty(monMatrix))
                    monMatrix = monMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(tueMatrix))
                    tueMatrix = tueMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(wedMatrix))
                    wedMatrix = wedMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(thuMatrix))
                    thuMatrix = thuMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(friMatrix))
                    friMatrix = friMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(satMatrix))
                    satMatrix = satMatrix.TrimEnd(',');

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_ClassTimeTable_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassSectionId", model.ClassSectionId);
                cmd.Parameters.AddWithValue("@MondayMatrix", monMatrix);
                cmd.Parameters.AddWithValue("@TuesdayMatrix", tueMatrix);
                cmd.Parameters.AddWithValue("@WednesdayMatrix", wedMatrix);
                cmd.Parameters.AddWithValue("@ThursdayMatrix", thuMatrix);
                cmd.Parameters.AddWithValue("@FridayMatrix", friMatrix);
                cmd.Parameters.AddWithValue("@SaturdayMatrix", satMatrix);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<int, string> TimeTable_Setup_CRUD(TimeTable_SetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string monMatrix = ""; string tueMatrix = ""; string wedMatrix = "";
                string thuMatrix = ""; string friMatrix = ""; string satMatrix = "";

                for (int i = 0; i < model.PeriodDetailsList.Count; i++)
                {
                    if (model.PeriodDetailsList[i].PeriodInfoList.Count > 0 && !string.IsNullOrEmpty(model.PeriodDetailsList[i].PeriodInfoList[0].SubjectId)) // && !string.IsNullOrEmpty(model.PeriodDetailsList[i].PeriodInfoList[0].TeacherId)
                        monMatrix += model.PeriodDetailsList[i].PeriodName + "_" + model.PeriodDetailsList[i].PeriodInfoList[0].SubjectId + model.PeriodDetailsList[i].PeriodInfoList[0].TeacherId + ",";

                    if (model.PeriodDetailsList[i].PeriodInfoList.Count > 1 && !string.IsNullOrEmpty(model.PeriodDetailsList[i].PeriodInfoList[1].SubjectId))
                        tueMatrix += model.PeriodDetailsList[i].PeriodName + "_" + model.PeriodDetailsList[i].PeriodInfoList[1].SubjectId + model.PeriodDetailsList[i].PeriodInfoList[1].TeacherId + ",";

                    if (model.PeriodDetailsList[i].PeriodInfoList.Count > 2 && !string.IsNullOrEmpty(model.PeriodDetailsList[i].PeriodInfoList[2].SubjectId))
                        wedMatrix += model.PeriodDetailsList[i].PeriodName + "_" + model.PeriodDetailsList[i].PeriodInfoList[2].SubjectId + model.PeriodDetailsList[i].PeriodInfoList[2].TeacherId + ",";

                    if (model.PeriodDetailsList[i].PeriodInfoList.Count > 3 && !string.IsNullOrEmpty(model.PeriodDetailsList[i].PeriodInfoList[3].SubjectId))
                        thuMatrix += model.PeriodDetailsList[i].PeriodName + "_" + model.PeriodDetailsList[i].PeriodInfoList[3].SubjectId + model.PeriodDetailsList[i].PeriodInfoList[3].TeacherId + ",";

                    if (model.PeriodDetailsList[i].PeriodInfoList.Count > 4 && !string.IsNullOrEmpty(model.PeriodDetailsList[i].PeriodInfoList[4].SubjectId))
                        friMatrix += model.PeriodDetailsList[i].PeriodName + "_" + model.PeriodDetailsList[i].PeriodInfoList[4].SubjectId + model.PeriodDetailsList[i].PeriodInfoList[4].TeacherId + ",";

                    if (model.PeriodDetailsList[i].PeriodInfoList.Count > 5 && !string.IsNullOrEmpty(model.PeriodDetailsList[i].PeriodInfoList[5].SubjectId))
                        satMatrix += model.PeriodDetailsList[i].PeriodName + "_" + model.PeriodDetailsList[i].PeriodInfoList[5].SubjectId + model.PeriodDetailsList[i].PeriodInfoList[5].TeacherId + ",";
                }

                if (!string.IsNullOrEmpty(monMatrix))
                    monMatrix = monMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(tueMatrix))
                    tueMatrix = tueMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(wedMatrix))
                    wedMatrix = wedMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(thuMatrix))
                    thuMatrix = thuMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(friMatrix))
                    friMatrix = friMatrix.TrimEnd(',');

                if (!string.IsNullOrEmpty(satMatrix))
                    satMatrix = satMatrix.TrimEnd(',');

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_ClassTimeTable_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassSectionId", model.ClassSectionId);
                cmd.Parameters.AddWithValue("@MondayMatrix", monMatrix);
                cmd.Parameters.AddWithValue("@TuesdayMatrix", tueMatrix);
                cmd.Parameters.AddWithValue("@WednesdayMatrix", wedMatrix);
                cmd.Parameters.AddWithValue("@ThursdayMatrix", thuMatrix);
                cmd.Parameters.AddWithValue("@FridayMatrix", friMatrix);
                cmd.Parameters.AddWithValue("@SaturdayMatrix", satMatrix);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }



        public Tuple<int, string> TimeTable_Setup_CRUD_V2(string classsectionId, string p, string d, string pdValue, string userid,string mode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string monMatrix = ""; string tueMatrix = ""; string wedMatrix = "";
                string thuMatrix = ""; string friMatrix = ""; string satMatrix = "";


                if (d == "0")
                {
                    monMatrix = 'P' + p + '_' + pdValue;
                    if (!string.IsNullOrEmpty(monMatrix))
                        monMatrix=monMatrix.TrimEnd('_');
                }
                if (d == "1")
                {
                        tueMatrix = 'P' + p + '_' + pdValue;
                    if (!string.IsNullOrEmpty(tueMatrix))
                        tueMatrix=tueMatrix.TrimEnd('_');
                }
                if (d == "2")
                {
                        wedMatrix = 'P' + p + '_' + pdValue;
                    if (!string.IsNullOrEmpty(wedMatrix))
                        wedMatrix=wedMatrix.TrimEnd('_');

                }
                if (d == "3")
                {
                    
                        thuMatrix = 'P' + p + '_' + pdValue;
                    if (!string.IsNullOrEmpty(thuMatrix))
                        thuMatrix=thuMatrix.TrimEnd('_');
                }
                if (d == "4")
                {
                    
                        friMatrix = 'P' + p + '_' + pdValue;
                    if (!string.IsNullOrEmpty(friMatrix))
                        friMatrix=friMatrix.TrimEnd('_');
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_ClassTimeTable_CRUD_V2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassSectionId", classsectionId);
                cmd.Parameters.AddWithValue("@MondayMatrix", monMatrix);
                cmd.Parameters.AddWithValue("@TuesdayMatrix", tueMatrix);
                cmd.Parameters.AddWithValue("@WednesdayMatrix", wedMatrix);
                cmd.Parameters.AddWithValue("@ThursdayMatrix", thuMatrix);
                cmd.Parameters.AddWithValue("@FridayMatrix", friMatrix);
                cmd.Parameters.AddWithValue("@SaturdayMatrix", satMatrix);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@Mode", mode);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<bool, string> CheckStaffAvailabilityForTimeTableMapping(string StaffId, string PeriodName, int DayNo)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TimeTable_CheckStaffAvailability", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", StaffId);
                cmd.Parameters.AddWithValue("@PeriodNo", PeriodName);
                cmd.Parameters.AddWithValue("@DayNo", DayNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<bool, string>(Convert.ToBoolean(dt.Rows[0][0]), dt.Rows[0][1].ToString());
            }
        }
    }
}
