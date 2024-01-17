using Microsoft.AspNetCore.Mvc.Rendering;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services
{
    public interface YBSCompanyService
    {
        List<YBSCompany> GetUniqueYBSCompanys();
        List<SelectListItem> GetSelectListYBSCompanys();
        YBSCompany FindYBSCompanyById(int pkId);

    }
}
