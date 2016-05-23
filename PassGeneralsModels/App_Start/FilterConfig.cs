using System.Web;
using System.Web.Mvc;

namespace PassGeneralsModels
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //can remove requireshttp.. if you don't have SSL certificate in production
            //filters.Add(new RequireHttpsAttribute());
        }
    }
}
