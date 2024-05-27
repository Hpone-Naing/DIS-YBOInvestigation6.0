using System.Data;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface YBSDriverCourseDeliveryService
    {
        bool CreateYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery);
        PagingList<YBSDriverCourseDelivery> GetAllYBSDriverCourseDeliveriesWithPagin(string searchString, int? pageNo, int PageSize);
        bool DeleteYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery);
        YBSDriverCourseDelivery FindYBSDriverCourseDeliveriesById(int id);
        YBSDriverCourseDelivery FindYBSDriverCourseDeliveriesByIdEgerLoad(int id);
        bool EditYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery);
        DataTable MakeYBSDriverCourseDeliveriesExcelData(PagingList<YBSDriverCourseDelivery> yBSDriverCourseDeliverys, bool exportAll);
        public int GetTotalRecordByDriver(int driverPkId);
    }
}
