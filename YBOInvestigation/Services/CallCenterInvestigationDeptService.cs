using System.Data;
using YBOInvestigation.Classes;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface CallCenterInvestigationDeptService
    {
        bool CreateCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept);
        PagingList<CallCenterInvestigationDept> GetAllCallCenterInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize);
        bool DeleteCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept);
        CallCenterInvestigationDept FindCallCenterInvestigationDeptById(int id);
        CallCenterInvestigationDept FindCallCenterInvestigationDeptByIdEgerLoad(int id);
        bool EditCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept);
        DataTable MakeCallCenterInvestigationDeptExcelData(PagingList<CallCenterInvestigationDept> callCenterInvestigationDepts, bool exportAll);
        public int GetTotalRecordByDriver(int driverPkId);
    }
}
