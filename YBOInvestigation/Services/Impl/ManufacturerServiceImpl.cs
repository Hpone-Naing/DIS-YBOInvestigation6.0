using YBOInvestigation.Data;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services.Impl
{
    public class ManufacturerServiceImpl : AbstractServiceImpl<Manufacturer>, ManufacturerService
    {
        private readonly ILogger<ManufacturerServiceImpl> _logger;
        public ManufacturerServiceImpl(YBOInvestigationDBContext context, ILogger<ManufacturerServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][GetAllManufacturers] Get Manufacturer list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get Manufacturer list. <<<<<<<<<<");
                return GetAll().Where(manufacturer => manufacturer.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving Manufacturer list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<Manufacturer> GetAllManufacturersWithPagin(int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][GetAllManufacturersWithPagin] Get Manufacturer pagination list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get Manufacturer pagination list. <<<<<<<<<<");
                return GetAllWithPagin(GetAllManufacturers(), pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing Manufacturer pagination list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<Manufacturer> GetUniqueManufacturers()
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][GetUniqueManufacturers] Get unique Manufacturer list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique Manufacturer list. <<<<<<<<<<");
                return GetUniqueList(manufacturer => manufacturer.ManufacturerPkid);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique Manufacturer list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public Manufacturer FindManufacturerById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][FindManufacturerById] Find Manufacturer by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find Manufacturer by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding Manufacturer by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateManufacturer(Manufacturer manufacturer)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][CreateManufacturer] Create Manufacturer. <<<<<<<<<<");
            try
            {
                manufacturer.IsDeleted = false;
                _logger.LogInformation($">>>>>>>>>> Success. Create Manufacturer. <<<<<<<<<<");
                return Create(manufacturer);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating Manufacturer. <<<<<<<<<<" + e);
                throw;
            }
        }
        public bool EditManufacturer(Manufacturer manufacturer)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][EditManufacturer] Edit Manufacturer. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit Manufacturer. <<<<<<<<<<");
                return Update(manufacturer);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating Manufacturer. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteManufacturer(Manufacturer manufacturer)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][DeleteManufacturer] Soft delete Manufacturer. <<<<<<<<<<");
            try
            {
                manufacturer.IsDeleted = true;
                _logger.LogInformation($">>>>>>>>>> Success. Soft delete Manufacturer. <<<<<<<<<<");
                return Update(manufacturer);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting Manufacturer. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
