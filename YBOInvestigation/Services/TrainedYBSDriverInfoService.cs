using YBOInvestigation.Models;

namespace YBOInvestigation.Services
{
    public interface TrainedYBSDriverInfoService
    {
        public TrainedYBSDriverInfo FindTrainedYBSDriverInfoById(int driverPkId);
        public List<TrainedYBSDriverInfo> GetTrainedYBSDriverInfosByDriverId(int driverPkId);
        public TrainedYBSDriverInfo GetTrainedYBSDriverInfoByDriverId(int driverPkId);
        public TrainedYBSDriverInfo GetTrainedYBSDriverInfoByDriverIdEgerLoad(int driverPkId);
        public bool CreateTrainedYBSDriverInfo(TrainedYBSDriverInfo driver);

        public bool EditTrainedYBSDriverInfo(TrainedYBSDriverInfo driver);
    }
}
