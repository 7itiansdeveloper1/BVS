using System.Collections.Generic;
using ISas.Entities;
using System.Data;
using System;
using System.Web.Mvc;

namespace ISas.Repository.Interface
{
    public interface IStudentUpdation
    {
        IEnumerable<UpdationParametersList> GetUpdationParametersList(string sessionid, string classid, string userid);
        Tuple<IEnumerable<ClassofJoiningList>, string> GetClassofJoiningList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<ReligionList> GetReligionList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<CategoryList1> GetCategoryList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<HouseList> GetHouseList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<BloodGroupList> GetBloodGroupList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<ProfessionList> GetProfessionList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<ModeofTransportList> GetModeofTransportList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<PickedUpByList> GetPickedUpByList(string sessionid, string classid, string sectionid, string userid, string mode);
        Tuple<int, string> StudentUpdation_CRUD(DataTable dt, string userid, string mode);
        List<SelectListItem> GetClassSectionList(string sessionid, string classid, string sectionid, string userid, string mode);
        List<SelectListItem> GetClubList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<StreamList> GetStreamList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<SelectListItem> GetSnacksList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<SelectListItem> GetDefaulterList(string sessionid, string classid, string sectionid, string userid, string mode);
    }
}
