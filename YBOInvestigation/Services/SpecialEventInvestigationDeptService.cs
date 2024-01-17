using System.Data;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface SpecialEventInvestigationDeptService
    {
        bool CreateSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept);
        PagingList<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize);
        bool DeleteSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept);
        SpecialEventInvestigationDept FindSpecialEventInvestigationDeptById(int id);
        SpecialEventInvestigationDept FindSpecialEventInvestigationDeptByIdEgerLoad(int id);
        bool EditSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept);
        DataTable MakeSpecialEventInvestigationDeptExcelData(PagingList<SpecialEventInvestigationDept> specialEventInvestigationDepts, bool exportAll);
    }
}
