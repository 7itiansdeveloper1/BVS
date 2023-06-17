using ISas.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Interface
{
    public interface ICommon_AttachmentRefRepo
    {
        List<Common_AttachemntRefrence> Common_AttachmentRef_List(string refId, string refId1, string filterBy);
        Tuple<int, string> Common_AttachmentRef_DELETE(string key);
        Tuple<int, string> Common_AttachmentRef_ADD(Common_AttachemntRefrence model);
        List<Common_AttachemntRefrence> Common_AttachmentRef_Stud_List(string refId, string refId1, string filterBy);
    }
}
