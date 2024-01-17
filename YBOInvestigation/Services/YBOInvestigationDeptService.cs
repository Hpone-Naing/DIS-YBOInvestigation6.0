using System.Data;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface YBOInvestigationDeptService
    {
        bool CreateYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept);
        PagingList<YBOInvestigationDept> GetAllYBOInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize);
        bool DeleteYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept);
        YBOInvestigationDept FindYBOInvestigationDeptById(int id);
        YBOInvestigationDept FindYBOInvestigationDeptByIdEgerLoad(int id);
        bool EditYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept);
        DataTable MakeYBOInvestigationDeptExcelData(PagingList<YBOInvestigationDept> yBOInvestigationDepts, bool exportAll);
    }
}
