using Microsoft.AspNetCore.Mvc.Rendering;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services
{
    public interface YBSTypeService
    {
        List<YBSType> GetUniqueYBSTypes();
        List<YBSType> GetUniqueYBSTypesByYBSCompanyId(int ybsCompanyId = 1);
        List<SelectListItem> GetSelectListYBSTypesByYBSCompanyId(int ybsCompanyId = 1);
        YBSType FindYBSTypeById(int pkId);
        List<YBSType> GetAllYBSTypes();
    }
}
