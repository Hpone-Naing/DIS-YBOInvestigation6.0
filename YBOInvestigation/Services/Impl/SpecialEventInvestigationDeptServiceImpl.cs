using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace YBOInvestigation.Services.Impl
{
    public class SpecialEventInvestigationDeptServiceImpl : AbstractServiceImpl<SpecialEventInvestigationDept>, SpecialEventInvestigationDeptService
    {
        private readonly ILogger<SpecialEventInvestigationDeptServiceImpl> _logger;
        private readonly VehicleDataService _vehicleDataService;
        public SpecialEventInvestigationDeptServiceImpl(YBOInvestigationDBContext context, ILogger<SpecialEventInvestigationDeptServiceImpl> logger, VehicleDataService vehicleDataService) : base(context, logger)
        {
            _logger = logger;
            _vehicleDataService = vehicleDataService;
        }

        public List<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDepts()
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][GetAllSpecialEventInvestigationDepts] Get SpecialEventInvestigationDept list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get SpecialEventInvestigationDept list. <<<<<<<<<<");
                return GetAll().Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false).ToList();
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving SpecialEventInvestigationDept list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDeptsEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][GetAllSpecialEventInvestigationDeptsEgerLoad] Get SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                return _context.SpecialEventInvestigationDepts
                        .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false)
                                .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                                .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                                .ToList();
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving SpecialEventInvestigationDept eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][GetAllSpecialEventInvestigationDeptsWithPagin] SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
            try
            {
                List<SpecialEventInvestigationDept> resultList = new List<SpecialEventInvestigationDept>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                   
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
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
                    catch(Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.SpecialEventInvestigationDepts
                            .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                                    .ToList();
                    }
                    catch(Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> Success. SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }


        public bool CreateSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][CreateSpecialEventInvestigationDept] Create SpecialEventInvestigationDept <<<<<<<<<<");
            try
            {
                specialEventInvestigationDept.IsDeleted = false;
                specialEventInvestigationDept.CreatedDate = DateTime.Now;
                specialEventInvestigationDept.CreatedBy = "admin";
                _logger.LogInformation(">>>>>>>>>> Create success SpecialEventInvestigationDept. <<<<<<<<<<");
                return Create(specialEventInvestigationDept);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating SpecialEventInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][EditSpecialEventInvestigationDept] Edit SpecialEventInvestigationDept <<<<<<<<<<");
            try
            {
                specialEventInvestigationDept.IsDeleted = false;
                specialEventInvestigationDept.CreatedDate = DateTime.Now;
                specialEventInvestigationDept.CreatedBy = "admin";
                _logger.LogInformation(">>>>>>>>>> Success update SpecialEventInvestigationDept. <<<<<<<<<<");
                return Update(specialEventInvestigationDept);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating SpecialEventInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][DeleteSpecialEventInvestigationDept] Soft delete SpecialEventInvestigationDept <<<<<<<<<<");
            try
            {
                specialEventInvestigationDept.IsDeleted = true;
                _logger.LogInformation(">>>>>>>>>> Success soft delete SpecialEventInvestigationDept. <<<<<<<<<<");
                return Update(specialEventInvestigationDept);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting SpecialEventInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public SpecialEventInvestigationDept FindSpecialEventInvestigationDeptById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][FindSpecialEventInvestigationDeptById] Find SpecialEventInvestigationDept by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find SpecialEventInvestigationDept by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding SpecialEventInvestigationDept by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public SpecialEventInvestigationDept FindSpecialEventInvestigationDeptByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][FindSpecialEventInvestigationDeptById] Find SpecialEventInvestigationDept by pkId with eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find SpecialEventInvestigationDept by pkId with eger load. <<<<<<<<<<");
                return _context.SpecialEventInvestigationDepts.Where(record => record.IsDeleted == false)
                               .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                               .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                               .FirstOrDefault(specialEventInvestigationDept => specialEventInvestigationDept.SpecialEventInvestigationDeptDeptPkid == id);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding SpecialEventInvestigationDept by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public DataTable MakeSpecialEventInvestigationDeptExcelData(PagingList<SpecialEventInvestigationDept> specialEventInvestigationDepts, bool exportAll)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][MakeSpecialEventInvestigationDeptExcelData] Expor excel SpecialEventInvestigationDept. <<<<<<<<<<");
            DataTable dt = new DataTable("ထူးခြားဖြစ်စဥ် စာရင်း");
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
            {
                _logger.LogInformation(">>>>>>>>>> For export all datas. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> ှSuccess. Get all SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                    list = GetAllSpecialEventInvestigationDeptsEgerLoad();
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all SpecialEventInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation(">>>>>>>>>> For export paginate or searchResult SpecialEventInvestigationDept list. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all paginate or searchResult SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success. Get all paginate or searchResult SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                    list = specialEventInvestigationDepts;
                }
                catch(Exception e)
                {
                _logger.LogError(">>>>>>>>>> Error occur when getting all paginate or searchResult SpecialEventInvestigationDept eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable. <<<<<<<<<<");
                if (list.Count() > 0)
                {
                    foreach (var specialEventInvestigationDept in list)
                    {
                        dt.Rows.Add(specialEventInvestigationDept.IncidenceDate, specialEventInvestigationDept.IncidenceTime, specialEventInvestigationDept.IncidencePlace, specialEventInvestigationDept.VehicleNumber, specialEventInvestigationDept.ReportTime, specialEventInvestigationDept.YbsRecordSender, specialEventInvestigationDept.YbsRecordSenderPosition, specialEventInvestigationDept.RecordDescription, specialEventInvestigationDept.YBSCompany.YBSCompanyName, specialEventInvestigationDept.YBSType.YBSTypeName);
                    }
                }
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable success. <<<<<<<<<<");
                return dt;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when assigning SearchAll or GetAll CallCenterInvestigationDept list to dataTable. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
