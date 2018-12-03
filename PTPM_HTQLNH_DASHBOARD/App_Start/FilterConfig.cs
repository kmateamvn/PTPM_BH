using System.Web;
using System.Web.Mvc;

namespace PTPM_HTQLNH_DASHBOARD
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
