using System.Data;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface TrafficControlCenterInvestigationDeptService
    {
        bool CreateTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept);
        PagingList<TrafficControlCenterInvestigationDept> GetAllTrafficControlCenterInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize);
        bool DeleteTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept);
        TrafficControlCenterInvestigationDept FindTrafficControlCenterInvestigationDeptById(int id);
        TrafficControlCenterInvestigationDept FindTrafficControlCenterInvestigationDeptByIdEgerLoad(int id);
        bool EditTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept);
        DataTable MakeTrafficControlCenterInvestigationDeptExcelData(PagingList<TrafficControlCenterInvestigationDept> trafficControlCenterInvestigationDepts, bool exportAll);
        public int GetTotalRecordByDriver(int driverPkId);
    }
}
