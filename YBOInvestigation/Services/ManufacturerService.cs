using YBOInvestigation.Models;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services
{
    public interface ManufacturerService
    {
        List<Manufacturer> GetUniqueManufacturers();
        public PagingList<Manufacturer> GetAllManufacturersWithPagin(int? pageNo, int PageSize);
        public Manufacturer FindManufacturerById(int id);
        public bool CreateManufacturer(Manufacturer manufacturer);
        public bool DeleteManufacturer(Manufacturer manufacturer);
        public bool EditManufacturer(Manufacturer manufacturer);



    }
}
