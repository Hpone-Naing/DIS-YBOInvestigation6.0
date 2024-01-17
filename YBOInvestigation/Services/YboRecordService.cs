using System.Data;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface YboRecordService
    {
        bool CreateYboRecord(YboRecord yboRecord);
        PagingList<YboRecord> GetAllYboRecordsWithPagin(string searchString, int? pageNo, int PageSize);
        bool DeleteYboRecord(YboRecord yboRecord);
        YboRecord FindYboRecordById(int id);
        YboRecord FindYboRecordByIdEgerLoad(int id);
        bool EditYboRecord(YboRecord yboRecord);
        DataTable MakeYboRecordExcelData(PagingList<YboRecord> yboRecords, bool exportAll);
    }
}
