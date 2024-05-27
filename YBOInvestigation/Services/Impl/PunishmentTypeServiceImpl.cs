using YBOInvestigation.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class PunishmentTypeServiceImpl : AbstractServiceImpl<PunishmentType>, PunishmentTypeService
    {
        private readonly ILogger<PunishmentTypeServiceImpl> _logger;
        public PunishmentTypeServiceImpl(YBOInvestigationDBContext context, ILogger<PunishmentTypeServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<PunishmentType> GetAllPunishmentTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][GetAllPunishmentTypes] Get PunishmentType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get PunishmentType list. <<<<<<<<<<");
                return GetAll().Where(punishmentType => punishmentType.IsDeleted == false).ToList();
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving PunishmentType list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<SelectListItem> GetSelectListPunishmentTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][GetSelectListPunishmentTypes] Make PunishmentType selectbox's options and values. <<<<<<<<<<");
            var lstPunishmentTypes = new List<SelectListItem>();
            List<PunishmentType> PunishmentTypes = new List<PunishmentType>();
            _logger.LogInformation($">>>>>>>>>> Get unique PunishmentType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique PunishmentType list. <<<<<<<<<<");
                PunishmentTypes = GetUniquePunishmentTypes();
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving unique PunishmentType list. <<<<<<<<<<" + e);
                throw;
            }
            _logger.LogInformation($">>>>>>>>>> Make PunishmentType selectbox's options and values. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Assign pkid and name to PunishmentType selectbox's values and options. <<<<<<<<<<");
                try
                {
                    lstPunishmentTypes = PunishmentTypes.Select(punishmentType => new SelectListItem()
                    {
                        Value = punishmentType.PunishmentTypePkid.ToString(),
                        Text = punishmentType.Punishment
                    }).ToList();
                    _logger.LogInformation($">>>>>>>>>>Success. Assign pkid and name to PunishmentType selectbox's values and options. <<<<<<<<<<");
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when assigning pkid and name to PunishmentType selectbox's values and options. <<<<<<<<<<" + e);
                    throw;
                }
                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "-----ရွေးချယ်ရန်-----"
                };

                lstPunishmentTypes.Insert(0, defItem);
                _logger.LogInformation($">>>>>>>>>> Success. Make PunishmentType selectbox's options and values. <<<<<<<<<<");
                return lstPunishmentTypes;
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when making PunishmentType selectbox's options and values. <<<<<<<<<<" + e);
                throw;
            }
        }



        public PagingList<PunishmentType> GetAllPunishmentTypesWithPagin(int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][GetAllPunishmentTypesWithPagin] Get PunishmentType pagination list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get PunishmentType pagination list. <<<<<<<<<<");
                return GetAllWithPagin(GetAllPunishmentTypes(), pageNo, PageSize);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing PunishmentType pagination list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<PunishmentType> GetUniquePunishmentTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][GetUniquePunishmentTypes] Get unique PunishmentType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique PunishmentType list. <<<<<<<<<<");
                return GetUniqueList(punishmentType => punishmentType.PunishmentTypePkid).Where(punishmentType => punishmentType.IsDeleted == false).ToList();
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique PunishmentType list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PunishmentType FindPunishmentTypeById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][FindPunishmentTypeById] Find PunishmentType by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find PunishmentType by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding PunishmentType by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<SelectListItem> GetPunishmentTypeSelectList(List<PunishmentType> punishmentTypes, string value, string text)
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][GetPunishmentTypeSelectList] Make PunishmentType selectbox's options and values. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Make PunishmentType selectbox's options and values. <<<<<<<<<<");
                return GetItemsFromList(punishmentTypes, value, text);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when making PunishmentType selectbox's options and values. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreatePunishmentType(PunishmentType punishmentType)
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][CreatePunishmentType] Create PunishmentType. <<<<<<<<<<");
            try
            {
                punishmentType.IsDeleted = false;
                _logger.LogInformation($">>>>>>>>>> Success. Create PunishmentType. <<<<<<<<<<");
                return Create(punishmentType);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating PunishmentType. <<<<<<<<<<" + e);
                throw;
            }
        }
        public bool EditPunishmentType(PunishmentType punishmentType)
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][EditPunishmentType] Edit PunishmentType. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit PunishmentType. <<<<<<<<<<");
                return Update(punishmentType);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating PunishmentType. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeletePunishmentType(PunishmentType punishmentType)
        {
            _logger.LogInformation(">>>>>>>>>> [PunishmentTypeServiceImpl][EditPunishmentType] Soft delete PunishmentType. <<<<<<<<<<");
            try
            {
                punishmentType.IsDeleted = true;
                _logger.LogInformation($">>>>>>>>>> Success. Soft delete PunishmentType. <<<<<<<<<<");
                return Update(punishmentType);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting PunishmentType. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
