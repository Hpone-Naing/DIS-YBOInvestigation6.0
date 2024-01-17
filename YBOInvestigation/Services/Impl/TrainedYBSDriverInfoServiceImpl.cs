using Microsoft.EntityFrameworkCore;
using YBOInvestigation.Data;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class TrainedYBSDriverInfoServiceImpl : AbstractServiceImpl<TrainedYBSDriverInfo>, TrainedYBSDriverInfoService
    {
        public TrainedYBSDriverInfoServiceImpl(YBOInvestigationDBContext context) : base(context)
        {
        }

        public TrainedYBSDriverInfo FindTrainedYBSDriverInfoById(int driverPkId)
        {
            return FindById(driverPkId);
        }

        public List<TrainedYBSDriverInfo> GetTrainedYBSDriverInfosByDriverId(int driverPkId)
        {
            return GetListByIntVal("DriverPkid", driverPkId);
        }

        public TrainedYBSDriverInfo GetTrainedYBSDriverInfoByDriverId(int driverPkId)
        {
            return GetObjByIntVal("DriverPkid", driverPkId);
        }

        public TrainedYBSDriverInfo GetTrainedYBSDriverInfoByDriverIdEgerLoad(int driverPkId)
        {
            return _context.TrainedYBSDriverInfos
                .Include(trainedDriver => trainedDriver.Driver)
                .FirstOrDefault(trainedDriver => trainedDriver.Driver.DriverPkid == driverPkId);
        }

        public bool CreateTrainedYBSDriverInfo(TrainedYBSDriverInfo driver)
        {
            return Create(driver);
        }

        public bool EditTrainedYBSDriverInfo(TrainedYBSDriverInfo driver)
        {
            return Update(driver);
        }
    }
}
