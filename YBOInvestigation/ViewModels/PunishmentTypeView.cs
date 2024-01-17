using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.ViewModels
{
    public class PunishmentTypeView
    {
        public PagingList<PunishmentType> PunishmentTypeList { get; set; }
        public PunishmentType PunishmentType{ get; set; }
    }
}
