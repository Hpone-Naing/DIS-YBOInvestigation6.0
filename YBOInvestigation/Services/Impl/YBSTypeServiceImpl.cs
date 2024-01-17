using YBOInvestigation.Data;
using YBOInvestigation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace YBOInvestigation.Services.Impl
{
    public class YBSTypeServiceImpl : AbstractServiceImpl<YBSType>, YBSTypeService
    {
        public YBSTypeServiceImpl(YBOInvestigationDBContext context) : base(context)
        {
        }

        public List<YBSType> GetAllYBSTypes()
        {
            return GetAll();
        }

        public List<YBSType> GetUniqueYBSTypes()
        {
            return GetUniqueList(ybsType => ybsType.YBSTypePkid);
        }

        public List<YBSType> GetUniqueYBSTypesByYBSCompanyId(int ybsCompanyId = 1)
        {
            return GetListByIntVal("YBSCompanyPkid", ybsCompanyId);
        }

        public List<SelectListItem> GetSelectListYBSTypesByYBSCompanyId(int ybsCompanyId = 1)
        {
            List<SelectListItem> lstYBSTypes = GetUniqueYBSTypesByYBSCompanyId(ybsCompanyId)
                .Select(
                    ybsType => new SelectListItem
                    {
                        Value = ybsType.YBSTypePkid.ToString(),
                        Text = ybsType.YBSTypeName
                    }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-----ရွေးချယ်ရန်-----"
            };

            lstYBSTypes.Insert(0, defItem);
            return lstYBSTypes;
        }

        public YBSType FindYBSTypeById(int pkId)
        {
            return FindById(pkId);
        }

        public bool CreateYBSType(YBSType ybsType)
        {
            return Create(ybsType);
        }
    }
}
