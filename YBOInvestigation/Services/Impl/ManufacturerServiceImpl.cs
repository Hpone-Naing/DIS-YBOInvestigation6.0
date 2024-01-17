using YBOInvestigation.Data;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services.Impl
{
    public class ManufacturerServiceImpl : AbstractServiceImpl<Manufacturer>, ManufacturerService
    {
        public ManufacturerServiceImpl(YBOInvestigationDBContext context) : base(context)
        {
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return GetAll().Where(manufacturer => manufacturer.IsDeleted == false).ToList();
        }

        public PagingList<Manufacturer> GetAllManufacturersWithPagin(int? pageNo, int PageSize)
        {
            return GetAllWithPagin(GetAllManufacturers(), pageNo, PageSize);
        }

        public List<Manufacturer> GetUniqueManufacturers()
        {
            return GetUniqueList(manufacturer => manufacturer.ManufacturerPkid);
        }

        public Manufacturer FindManufacturerById(int id)
        {
            return FindById(id);
        }

        public bool CreateManufacturer(Manufacturer manufacturer)
        {
            manufacturer.IsDeleted = false;
            return Create(manufacturer);
        }
        public bool EditManufacturer(Manufacturer manufacturer)
        {
            return Update(manufacturer);
        }

        public bool DeleteManufacturer(Manufacturer manufacturer)
        {
            manufacturer.IsDeleted = true;
            return Update(manufacturer);
        }
    }
}
