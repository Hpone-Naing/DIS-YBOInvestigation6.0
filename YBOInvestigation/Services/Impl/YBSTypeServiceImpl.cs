using YBOInvestigation.Data;
using YBOInvestigation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace YBOInvestigation.Services.Impl
{
    public class YBSTypeServiceImpl : AbstractServiceImpl<YBSType>, YBSTypeService
    {
        private readonly ILogger<YBSTypeServiceImpl> _logger;

        public YBSTypeServiceImpl(YBOInvestigationDBContext context, ILogger<YBSTypeServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<YBSType> GetAllYBSTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetAllYBSTypes] Get YBSType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get YBSType list. <<<<<<<<<<");
                return GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving YBSType list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YBSType> GetUniqueYBSTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetUniqueYBSTypes] Get unique YBSType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique YBSType list. <<<<<<<<<<");
                return GetUniqueList(ybsType => ybsType.YBSTypePkid);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique YBSType list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YBSType> GetUniqueYBSTypesByYBSCompanyId(int ybsCompanyId = 1)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetUniqueYBSTypesByYBSCompanyId] Find YBSType by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find YBSType by pkId. <<<<<<<<<<");
                return GetListByIntVal("YBSCompanyPkid", ybsCompanyId);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSType by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<SelectListItem> GetSelectListYBSTypesByYBSCompanyId(int ybsCompanyId = 1)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetSelectListYBSTypesByYBSCompanyId] Get SelectList YBSType selectbox's options and values. <<<<<<<<<<");
            try
            {
                var lstYBSTypes = new List<SelectListItem>();
                _logger.LogInformation($">>>>>>>>>> Make YBSType selectbox's options and values. <<<<<<<<<<");
                try
                {
                    lstYBSTypes = GetUniqueYBSTypesByYBSCompanyId(ybsCompanyId)
                .Select(
                    ybsType => new SelectListItem
                    {
                        Value = ybsType.YBSTypePkid.ToString(),
                        Text = ybsType.YBSTypeName
                    }).ToList();
                    _logger.LogInformation($">>>>>>>>>> Success. Make YBSType selectbox's options and values. <<<<<<<<<<");

                }
                catch (Exception e)
                {
                    _logger.LogInformation($">>>>>>>>>> Error occur. Make YBSType selectbox's options and values. <<<<<<<<<<");
                    throw;
                }
                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "-----ရွေးချယ်ရန်-----"
                };

                lstYBSTypes.Insert(0, defItem);
                return lstYBSTypes;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. Get SelectList YBSType selectbox's options and values. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBSType FindYBSTypeById(int pkId)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][FindYBSTypeById] Find YBSType by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find YBSType by pkId. <<<<<<<<<<");
                return FindById(pkId);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSType by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        /*public bool CreateYBSType(YBSType ybsType)
        {
            return Create(ybsType);
        }*/
    }
}
