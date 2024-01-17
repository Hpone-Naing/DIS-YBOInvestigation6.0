using Microsoft.AspNetCore.Mvc.Rendering;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services
{
    public interface PunishmentTypeService
    {
        List<PunishmentType> GetUniquePunishmentTypes();
        List<SelectListItem> GetSelectListPunishmentTypes();
        public PagingList<PunishmentType> GetAllPunishmentTypesWithPagin(int? pageNo, int PageSize);
        public PunishmentType FindPunishmentTypeById(int id);
        public List<SelectListItem> GetPunishmentTypeSelectList(List<PunishmentType> punishmentTypes, string value, string text);
        public bool CreatePunishmentType(PunishmentType punishmentType);
        public bool DeletePunishmentType(PunishmentType punishmentType);
        public bool EditPunishmentType(PunishmentType punishmentType);

    }
}
