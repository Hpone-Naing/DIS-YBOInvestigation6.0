using YBOInvestigation.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class PunishmentTypeServiceImpl : AbstractServiceImpl<PunishmentType>, PunishmentTypeService
    {
        public PunishmentTypeServiceImpl(YBOInvestigationDBContext context) : base(context)
        {
        }

        public List<PunishmentType> GetAllPunishmentTypes()
        {
            return GetAll().Where(punishmentType => !punishmentType.IsDeleted).ToList();
        }

        public List<SelectListItem> GetSelectListPunishmentTypes()
        {
            var lstPunishmentTypes = new List<SelectListItem>();
            List<PunishmentType> PunishmentTypes = GetUniquePunishmentTypes();
            lstPunishmentTypes = PunishmentTypes.Select(punishmentType => new SelectListItem()
            {
                Value = punishmentType.PunishmentTypePkid.ToString(),
                Text = punishmentType.Punishment
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-----ရွေးချယ်ရန်-----"
            };

            lstPunishmentTypes.Insert(0, defItem);
            return lstPunishmentTypes;
        }



        public PagingList<PunishmentType> GetAllPunishmentTypesWithPagin(int? pageNo, int PageSize)
        {
            return GetAllWithPagin(GetAllPunishmentTypes(), pageNo, PageSize);
        }

        public List<PunishmentType> GetUniquePunishmentTypes()
        {
            return GetUniqueList(punishmentType => punishmentType.PunishmentTypePkid).Where(punishmentType => !punishmentType.IsDeleted).ToList();
        }

        public PunishmentType FindPunishmentTypeById(int id)
        {
            return FindById(id);
        }

        public List<SelectListItem> GetPunishmentTypeSelectList(List<PunishmentType> punishmentTypes, string value, string text)
        {
            return GetItemsFromList(punishmentTypes, value, text);
        }

        public bool CreatePunishmentType(PunishmentType punishmentType)
        {
            punishmentType.IsDeleted = false;
            return Create(punishmentType);
        }
        public bool EditPunishmentType(PunishmentType punishmentType)
        {
            return Update(punishmentType);
        }

        public bool DeletePunishmentType(PunishmentType punishmentType)
        {
            punishmentType.IsDeleted = true;
            return Update(punishmentType);
        }
    }
}
