using System.Web;
using System.Web.Mvc;

namespace GlobaHr_SendEmail_API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
