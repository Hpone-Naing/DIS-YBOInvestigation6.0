using YBOInvestigation.Classes;
using Microsoft.AspNetCore.Mvc;

namespace YBOInvestigation.Util
{
    public class Utility
    {
        public static int DEFAULT_PAGINATION_NUMBER = 5;

        public static void AlertMessage(Controller controller, string message, string color)
        {
            controller.TempData["Message"] = message;
            controller.TempData["CssColor"] = color;
        }

        public static AdvanceSearch MakeAdvanceSearch(HttpContext context)
        {
            string cngQty = context.Request.Query["CngQty"];
            string cctvInstalled = context.Request.Query["CctvInstalled"];
            string totalBusStop = context.Request.Query["TotalBusStop"];
            string totalBusStopOption = context.Request.Query["TotalBusStopOption"];

            AdvanceSearch advanceSearch = new AdvanceSearch();
            advanceSearch.CngQty = cngQty;
            advanceSearch.CctvInstalled = cctvInstalled;
            advanceSearch.TotalBusStop = totalBusStop;
            advanceSearch.TotalBusStopOption = totalBusStopOption;
            return advanceSearch;
        }

    }
}
