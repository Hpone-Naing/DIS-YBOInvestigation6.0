using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace YBOInvestigation.Services.Impl
{
    public class SpecialEventInvestigationDeptServiceImpl : AbstractServiceImpl<SpecialEventInvestigationDept>, SpecialEventInvestigationDeptService
    {
        private readonly VehicleDataService _vehicleDataService;
        public SpecialEventInvestigationDeptServiceImpl(YBOInvestigationDBContext context, VehicleDataService vehicleDataService) : base(context)
        {
            _vehicleDataService = vehicleDataService;
        }

        public List<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDepts()
        {
            return GetAll().Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false).ToList();
        }

        public List<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDeptsEgerLoad()
        {
            return _context.SpecialEventInvestigationDepts
                    .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false)
                            .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                            .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                            .ToList();
        }

        public PagingList<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            List<SpecialEventInvestigationDept> specialEventInvestigationDepts = GetAllSpecialEventInvestigationDepts();
            List<SpecialEventInvestigationDept> resultList = new List<SpecialEventInvestigationDept>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
                resultList = _context.SpecialEventInvestigationDepts
                    .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false)
                            .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                            .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                            .ToList()
                            .Where(specialEventInvestigationDept => IsSearchDataContained(specialEventInvestigationDept, searchString)
                            || IsSearchDataContained(specialEventInvestigationDept.YBSCompany, searchString)
                            || IsSearchDataContained(specialEventInvestigationDept.YBSType, searchString)
                            ).AsQueryable().ToList();
            }
            else
            {
                resultList = _context.SpecialEventInvestigationDepts
                    .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false)
                            .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                            .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                            .ToList();
            }
            return GetAllWithPagin(resultList, pageNo, PageSize);
        }


        public bool CreateSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            specialEventInvestigationDept.IsDeleted = false;
            specialEventInvestigationDept.CreatedDate = DateTime.Now;
            specialEventInvestigationDept.CreatedBy = "admin";
            
            return Create(specialEventInvestigationDept);

        }

        public bool EditSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            specialEventInvestigationDept.IsDeleted = false;
            specialEventInvestigationDept.CreatedDate = DateTime.Now;
            specialEventInvestigationDept.CreatedBy = "admin";
            return Update(specialEventInvestigationDept);
        }

        public bool DeleteSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            specialEventInvestigationDept.IsDeleted = true;
            return Update(specialEventInvestigationDept);
        }

        public SpecialEventInvestigationDept FindSpecialEventInvestigationDeptById(int id)
        {
            return FindById(id);
        }

        public SpecialEventInvestigationDept FindSpecialEventInvestigationDeptByIdEgerLoad(int id)
        {
            return _context.SpecialEventInvestigationDepts.Where(record => record.IsDeleted == false)
                           .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                           .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                           .FirstOrDefault(specialEventInvestigationDept => specialEventInvestigationDept.SpecialEventInvestigationDeptDeptPkid == id);
        }

        public DataTable MakeSpecialEventInvestigationDeptExcelData(PagingList<SpecialEventInvestigationDept> specialEventInvestigationDepts, bool exportAll)
        {
            DataTable dt = new DataTable("YBO ဖမ်းစီးမှုစာရင်း");
            dt.Columns.AddRange(new DataColumn[10] {
                                        new DataColumn("ဖြစ်ပွားရက်စွဲ"),
                                        new DataColumn("ဖြစ်ပွားသည့်အချိန်"),
                                        new DataColumn("ဖြစ်ပွားသည့်နေရာ"),
                                        new DataColumn("ယာဥ်အမှတ်"),
                                        new DataColumn("သတင်းပေးပို့သည့်အချိန်"),
                                        new DataColumn("သတင်းပေးပို့သူအမည်"),
                                        new DataColumn("သတင်းပေးပို့သူရာထူး"),
                                        new DataColumn("ဖြစ်စဥ်အကျဥ်း"),
                                        new DataColumn("YBS Company Name"),
                                        new DataColumn("ယာဥ်လိုင်း"),
                                        });
            var list = new List<SpecialEventInvestigationDept>();
            if (exportAll)
                list = GetAllSpecialEventInvestigationDeptsEgerLoad();
            else
                list = specialEventInvestigationDepts;
            if (list.Count() > 0)
            {
                foreach (var specialEventInvestigationDept in list)
                {
                    dt.Rows.Add(specialEventInvestigationDept.IncidenceDate, specialEventInvestigationDept.IncidenceTime, specialEventInvestigationDept.IncidencePlace, specialEventInvestigationDept.VehicleNumber, specialEventInvestigationDept.ReportTime, specialEventInvestigationDept.YbsRecordSender, specialEventInvestigationDept.YbsRecordSenderPosition, specialEventInvestigationDept.RecordDescription, specialEventInvestigationDept.YBSCompany.YBSCompanyName, specialEventInvestigationDept.YBSType.YBSTypeName);
                }
            }
            return dt;

        }
    }
}
