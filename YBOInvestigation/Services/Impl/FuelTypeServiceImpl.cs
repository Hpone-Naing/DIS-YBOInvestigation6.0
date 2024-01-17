using YBOInvestigation.Data;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class FuelTypeServiceImpl : AbstractServiceImpl<FuelType>, FuelTypeService
    {
        public FuelTypeServiceImpl(YBOInvestigationDBContext context) : base(context)
        {
        }

        public List<FuelType> GetAllFuelTypes()
        {
            return GetAll();
        }

        public List<FuelType> GetUniqueFuelTypes()
        {
            return GetUniqueList(fuelType => fuelType.FuelTypePkid);
        }

        public bool CreateFuelType(FuelType fuelType)
        {
            return Create(fuelType);
        }
    }
}
