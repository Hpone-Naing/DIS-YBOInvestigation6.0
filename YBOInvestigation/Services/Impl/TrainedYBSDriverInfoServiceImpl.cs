using Microsoft.EntityFrameworkCore;
using YBOInvestigation.Data;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class TrainedYBSDriverInfoServiceImpl : AbstractServiceImpl<TrainedYBSDriverInfo>, TrainedYBSDriverInfoService
    {
        private readonly ILogger<TrainedYBSDriverInfoServiceImpl> _logger;
        public TrainedYBSDriverInfoServiceImpl(YBOInvestigationDBContext context, ILogger<TrainedYBSDriverInfoServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public TrainedYBSDriverInfo FindTrainedYBSDriverInfoById(int driverPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [TrainedYBSDriverInfoServiceImpl][FindTrainedYBSDriverInfoById] Find TrainedYBSDriverInfo by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find Manufacturer by pkId. <<<<<<<<<<");
                return FindById(driverPkId);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding Manufacturer by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<TrainedYBSDriverInfo> GetTrainedYBSDriverInfosByDriverId(int driverPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [TrainedYBSDriverInfoServiceImpl][GetTrainedYBSDriverInfosByDriverId] Get TrainedYBSDriverInfo list by driverPkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get TrainedYBSDriverInfo list by driverPkId. <<<<<<<<<<");
                return GetListByIntVal("DriverPkid", driverPkId);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing TrainedYBSDriverInfo list by driverPkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public TrainedYBSDriverInfo GetTrainedYBSDriverInfoByDriverId(int driverPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [TrainedYBSDriverInfoServiceImpl][GetTrainedYBSDriverInfoByDriverId] Find TrainedYBSDriverInfo by driverPkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find TrainedYBSDriverInfo by driverPkId. <<<<<<<<<<");
                return GetObjByIntVal("DriverPkid", driverPkId);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding TrainedYBSDriverInfo by driverPkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public TrainedYBSDriverInfo GetTrainedYBSDriverInfoByDriverIdEgerLoad(int driverPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [TrainedYBSDriverInfoServiceImpl][GetTrainedYBSDriverInfoByDriverIdEgerLoad] Find TrainedYBSDriverInfo by driverPkId with eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find TrainedYBSDriverInfo by driverPkId with eger load. <<<<<<<<<<");
                return _context.TrainedYBSDriverInfos
                .Include(trainedDriver => trainedDriver.Driver)
                .FirstOrDefault(trainedDriver => trainedDriver.Driver.DriverPkid == driverPkId);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding TrainedYBSDriverInfo by driverPId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateTrainedYBSDriverInfo(TrainedYBSDriverInfo driver)
        {
            _logger.LogInformation(">>>>>>>>>> [TrainedYBSDriverInfoServiceImpl][CreateTrainedYBSDriverInfo] Create TrainedYBSDriverInfo. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Create TrainedYBSDriverInfo. <<<<<<<<<<");
                return Create(driver);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating TrainedYBSDriverInfo. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditTrainedYBSDriverInfo(TrainedYBSDriverInfo driver)
        {
            _logger.LogInformation(">>>>>>>>>> [TrainedYBSDriverInfoServiceImpl][EditTrainedYBSDriverInfo] Edit TrainedYBSDriverInfo. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Edit TrainedYBSDriverInfo. <<<<<<<<<<");
                return Update(driver);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating TrainedYBSDriverInfo. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
