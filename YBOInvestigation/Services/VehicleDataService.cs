using YBOInvestigation.Classes;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface VehicleDataService
    {
        bool CreateVehicle(VehicleData vehicleData);
        List<VehicleData> GetAllVehicles();
        PagingList<VehicleData> GetAllVehiclesWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize, string searchOption = null);
        bool DeleteVehicle(VehicleData vehicleData);
        VehicleData FindVehicleDataById(int id);
        VehicleData FindVehicleDataByIdEgerLoad(int id);
        VehicleData FindVehicleDataByIdYBSTableEgerLoad(int id);

        bool EditVehicle(VehicleData vehicleData);
        VehicleData FindVehicleByVehicleNumber(string vehicleNumer);
    }
}
