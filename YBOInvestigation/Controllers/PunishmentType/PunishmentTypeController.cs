using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using YBOInvestigation.Factories;
using YBOInvestigation.Util;
using YBOInvestigation.ViewModels;

namespace YBOInvestigation.Controllers.YBOInvestigationController
{
    public class PunishmentTypeController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public PunishmentTypeController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private PunishmentTypeView MakePunishmentTypeView(int? pageNo)
        {
            int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
            PagingList<PunishmentType> punishmentTypes = _serviceFactory.CreatePunishmentTypeService().GetAllPunishmentTypesWithPagin(pageNo, pageSize);
            if(punishmentTypes.Count() < 1)
            {
                int newPageNo = pageNo.HasValue ? pageNo.GetValueOrDefault() - 1 : 1;//pageNo.HasValue ? (pageNo.GetValueOrDefault() - 1) : pageNo.Value;
                punishmentTypes = _serviceFactory.CreatePunishmentTypeService().GetAllPunishmentTypesWithPagin(newPageNo, pageSize);
                punishmentTypes.PageIndex = newPageNo;
            }
            PunishmentTypeView viewModel = new PunishmentTypeView
            {
                PunishmentTypeList = punishmentTypes,
                PunishmentType = new PunishmentType()
            };
            return viewModel;
        }
        public IActionResult List(int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            return View("CRUD", MakePunishmentTypeView(pageNo));
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(PunishmentType punishmentType, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreatePunishmentTypeService().CreatePunishmentType(punishmentType))
            {
                Utility.AlertMessage(this, "Save Success", "alert-success");
                try
                {
                    return RedirectToAction("List", new { pageNo = pageNo });
                }
                catch (NullReferenceException ne)
                {
                    Utility.AlertMessage(this, "Data Issue. Please fill PunishmentType in database", "alert-danger");
                    return RedirectToAction("List", new { pageNo = pageNo });
                }
                catch (SqlException se)
                {
                    Utility.AlertMessage(this, "Internal Server Error", "alert-danger");
                    return View();
                }
            }
            else
            {
                Utility.AlertMessage(this, "Save Fail.Internal Server Error", "alert-danger");
                return View();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(int Id, PunishmentType punishmentType, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            punishmentType.PunishmentTypePkid = Id;
            if (_serviceFactory.CreatePunishmentTypeService().EditPunishmentType(punishmentType))
            {
                Utility.AlertMessage(this, "Edit Success", "alert-success");
                return RedirectToAction("List", new { pageNo = pageNo });
            }
            else
            {
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                return RedirectToAction("List", new { pageNo = pageNo });
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int Id, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            PunishmentType punishmentType = _serviceFactory.CreatePunishmentTypeService().FindPunishmentTypeById(Id);
            if (_serviceFactory.CreatePunishmentTypeService().DeletePunishmentType(punishmentType))
            {
                Utility.AlertMessage(this, "Delete Success", "alert-success");
                return RedirectToAction("List", new { pageNo = pageNo });
            }
            else
            {
                Utility.AlertMessage(this, "Delete Fail.Internal Server Error", "alert-danger");
                return RedirectToAction("List", new { pageNo = pageNo });
            }
        }
    }
}
