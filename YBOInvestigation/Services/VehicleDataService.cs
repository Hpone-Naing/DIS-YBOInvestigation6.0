﻿using YBOInvestigation.Classes;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface VehicleDataService
    {
        List<VehicleData> GetAllVehicles();
        PagingList<VehicleData> GetAllVehiclesWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize, string searchOption = null);
        VehicleData FindVehicleDataById(int id);
        VehicleData FindVehicleDataByIdEgerLoad(int id);
        VehicleData FindVehicleDataByIdYBSTableEgerLoad(int id);
        VehicleData FindVehicleDataByIdContainSoftDeleteEgerLoad(int id);
        VehicleData FindVehicleByVehicleNumber(string vehicleNumer);
    }
}
