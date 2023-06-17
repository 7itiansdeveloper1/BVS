using ISas.Entities;
using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_ReportRepo : IFee_ReportRepo
    {
        public Fee_ReportModels GetFee_Report_FormLoad(string ModuleName, string ReportName, string UserId, string sessionId)
        {
            Fee_ReportModels model = new Fee_ReportModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
                cmd.Parameters.AddWithValue("@ReportType", ReportName);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ClassSectionList = ds.Tables[0].AsEnumerable().Select(r => new ClassSectionModel
                {
                    ClassSectionId = r.Field<string>("Id"),
                    ClassSectionName = r.Field<string>("TextValue"),
                    WingId = r.Field<string>("WingId"),
                    ClassName = r.Field<string>("ClassName"),
                }).ToList();

                model.ReportNameList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ReportDisplayName"),
                    Value = r.Field<string>("ReportName"),
                }).ToList();

                model.FeeCategoryList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("StructID"),
                    Text = r.Field<string>("StructName"),
                }).ToList();

                model.FeeHeadList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("HeadId"),
                    Text = r.Field<string>("HeadName"),
                    Selected = true,
                }).ToList();

                if (model.FeeCategoryList != null)
                    model.FeeCategoryList.Add(new SelectListItem { Text = "ALL", Value = "-1", Selected = true });
            }
            return model;
        }

        public Fee_ReportModels GetStudentDetailReport(Fee_ReportModels model)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_Reports", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                if (!string.IsNullOrEmpty(model.FromDate))
                    cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(model.FromDate).Date);
                if (!string.IsNullOrEmpty(model.ToDate))
                    cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(model.ToDate).Date);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@ReportName", model.ReportName);
                cmd.Parameters.AddWithValue("@ClassSectionId", string.Join(",", model.ClassSectionIds));
                cmd.Parameters.AddWithValue("@SelectedFeeHeadsId", string.Join(",", model.SelectedFeeHeadsId));
                cmd.Parameters.AddWithValue("@FeeStructure", model.FeeCategory);
                cmd.Parameters.AddWithValue("@FeeType", model.FeeType);
                cmd.Parameters.AddWithValue("@Mode", model.FeeMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds != null)
                {

                    model.Header1 = ds.Tables[0].Rows[0][0].ToString();
                    model.Header2 = ds.Tables[0].Rows[0][1].ToString();
                    model.Header3 = ds.Tables[0].Rows[0][2].ToString();
                    model.Header4 = ds.Tables[0].Rows[0][3].ToString();
                    model.ImageURL = ds.Tables[0].Rows[0][4].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        model.SelectedReportName = ds.Tables[1].Rows[0][0].ToString();
                        if (ds.Tables[1].Columns.Count > 1)
                            model.IsCrystalEnabled = Convert.ToBoolean(ds.Tables[1].Rows[0][1].ToString());
                        else
                            model.IsCrystalEnabled = false;
                    }

                    if (model.IsCrystalEnabled)
                    {
                        model.HeaderNameList1.Add("");
                        List<string> rowVal = new List<string>();
                        rowVal.Add("This is a Custom Report");
                        model.ValueList1.Add(rowVal);

                        model.HeaderNameList2.Add("");
                        List<string> rowVal1 = new List<string>();
                        rowVal1.Add("");
                        model.ValueList2.Add(rowVal1);
                    }
                    else
                    {

                        if (ds.Tables[2] != null)
                        {
                            for (int i = 0; i < ds.Tables[2].Columns.Count; i++)
                            {
                                model.HeaderNameList1.Add(ds.Tables[2].Columns[i].ColumnName);
                            }

                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                List<string> rowVal = new List<string>();
                                for (int j = 0; j < ds.Tables[2].Columns.Count; j++)
                                {
                                    rowVal.Add(ds.Tables[2].Rows[i][j].ToString());
                                }
                                model.ValueList1.Add(rowVal);
                            }
                        }

                        if (ds.Tables[3] != null)
                        {
                            for (int i = 0; i < ds.Tables[3].Columns.Count; i++)
                            {
                                model.HeaderNameList2.Add(ds.Tables[3].Columns[i].ColumnName);
                            }

                            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                            {
                                List<string> rowVal = new List<string>();
                                for (int j = 0; j < ds.Tables[3].Columns.Count; j++)
                                {
                                    rowVal.Add(ds.Tables[3].Rows[i][j].ToString());
                                }
                                model.ValueList2.Add(rowVal);
                            }
                        }
                    }





                    //if (ds.Tables[4] != null)
                    //{
                    //    for (int i = 0; i < ds.Tables[4].Columns.Count; i++)
                    //    {
                    //        model.HeaderNameList3.Add(ds.Tables[4].Columns[i].ColumnName);
                    //    }

                    //    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                    //    {
                    //        List<string> rowVal = new List<string>();
                    //        for (int j = 0; j < ds.Tables[4].Columns.Count; j++)
                    //        {
                    //            rowVal.Add(ds.Tables[4].Rows[i][j].ToString());
                    //        }
                    //        model.ValueList3.Add(rowVal);
                    //    }
                    //}

                }

            }
            return model;
        }


        public DataSet GetFeeReport_Crystal(Fee_ReportModels model)
        {
            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_Reports", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(model.FromDate))
                    cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(model.FromDate).Date);
                if (!string.IsNullOrEmpty(model.ToDate))
                    cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(model.ToDate).Date);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@ReportName", model.ReportName);
                cmd.Parameters.AddWithValue("@ClassSectionId", string.Join(",", model.ClassSectionIds));
                cmd.Parameters.AddWithValue("@SelectedFeeHeadsId", string.Join(",", model.SelectedFeeHeadsId));
                cmd.Parameters.AddWithValue("@FeeStructure", model.FeeCategory);
                cmd.Parameters.AddWithValue("@FeeType", model.FeeType);
                cmd.Parameters.AddWithValue("@Mode", model.FeeMode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }
    }
}
