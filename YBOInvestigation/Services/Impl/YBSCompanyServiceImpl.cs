using YBOInvestigation.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class YBSCompanyServiceImpl : AbstractServiceImpl<YBSCompany>, YBSCompanyService
    {
        private readonly ILogger<YBSCompanyServiceImpl> _logger;

        public YBSCompanyServiceImpl(YBOInvestigationDBContext context, ILogger<YBSCompanyServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<YBSCompany> GetAllYBSCompanys()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][GetAllYBSCompanys] Get YBSCompany list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get YBSCompany list. <<<<<<<<<<");
                return GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving YBSCompany list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YBSCompany> GetUniqueYBSCompanys()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][GetUniqueYBSCompanys] Get unique YBSCompany list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique YBSCompany list. <<<<<<<<<<");
                return GetUniqueList(ybscompany => ybscompany.YBSCompanyPkid);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique YBSCompany list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBSCompany FindYBSCompanyById(int pkId)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][FindYBSCompanyById] Find YBSCompany by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find YBSCompany by pkId. <<<<<<<<<<");
                return FindById(pkId);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSCompany by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<SelectListItem> GetSelectListYBSCompanys()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][GetSelectListYBSCompanys] Get SelectList YBSCompany selectbox's options and values. <<<<<<<<<<");
            try
            {
                var lstYBSCompanys = new List<SelectListItem>();
            List<YBSCompany> YBSCompanys = GetUniqueYBSCompanys();
                _logger.LogInformation($">>>>>>>>>> Make YBSCompany selectbox's options and values. <<<<<<<<<<");
                try
                {
                    lstYBSCompanys = YBSCompanys.Select(ybsCompany => new SelectListItem()
                    {
                        Value = ybsCompany.YBSCompanyPkid.ToString(),
                        Text = ybsCompany.YBSCompanyName
                    }).ToList();
                    _logger.LogInformation($">>>>>>>>>> Success. Make YBSCompany selectbox's options and values. <<<<<<<<<<");

                }
                catch (Exception e)
                {
                    _logger.LogInformation($">>>>>>>>>> Error occur. Make YBSCompany selectbox's options and values. <<<<<<<<<<");
                    throw;
                }
                var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-----ရွေးချယ်ရန်-----"
            };

            lstYBSCompanys.Insert(0, defItem);
                _logger.LogInformation($">>>>>>>>>> Success. Make YBSCompany selectbox's options and values. <<<<<<<<<<");
                return lstYBSCompanys;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. Get SelectList YBSCompany selectbox's options and values. <<<<<<<<<<" + e);
                throw;
            }
        }

        /*public bool CreateYBSCompany(YBSCompany ybsCompany)
        {
            return Create(ybsCompany);
        }*/
    }
}
