using YBOInvestigation.Data;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class FuelTypeServiceImpl : AbstractServiceImpl<FuelType>, FuelTypeService
    {
        private readonly ILogger<FuelTypeServiceImpl> _logger;
        public FuelTypeServiceImpl(YBOInvestigationDBContext context, ILogger<FuelTypeServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<FuelType> GetAllFuelTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][GetAllFuelTypes] GetAll FuelType. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. GetAll FuelType. <<<<<<<<<<");
                return GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing FuelType. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<FuelType> GetUniqueFuelTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][GetUniqueFuelTypes] Get unique FuelType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique FuelType list. <<<<<<<<<<");
                return GetUniqueList(fuelType => fuelType.FuelTypePkid);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique PunishmentType list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateFuelType(FuelType fuelType)
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][CreateFuelType] Create FuelType. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Create FuelType. <<<<<<<<<<");
                return Create(fuelType);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating FuelType. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
