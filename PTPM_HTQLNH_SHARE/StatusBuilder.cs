using PTPM_HTQLNH_SHARE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTPM_HTQLNH_SHARE
{
    public class StatusBuilder
    {
        public static List<StatusModelView> createUserStatus()
        {
            var list = new List<StatusModelView>();
            list.Add(new StatusModelView { display = "Hoạt động", value = "1" });
            list.Add(new StatusModelView { display = "Bị khóa", value = "0" });
            return list;
        }
    }
}
