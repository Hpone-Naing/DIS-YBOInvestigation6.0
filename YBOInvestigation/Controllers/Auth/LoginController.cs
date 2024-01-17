using YBOInvestigation.Classes;
using YBOInvestigation.Factories;
using YBOInvestigation.Util;
using Microsoft.AspNetCore.Mvc;

namespace YBOInvestigation.Controllers.Auth
{
    public class LoginController : Controller
    {
        public ServiceFactory factoryBuilder;

        public LoginController(ServiceFactory serviceFactory)
        {
            factoryBuilder = serviceFactory;
        }
        public IActionResult Index()
        {
            ViewBag.IsRememberMe = SessionUtil.IsRememberMe(HttpContext);
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                var loginUser =  factoryBuilder.CreateUserService().FindUserByUserNameEgerLoad(user.UserID);
                if (loginUser != null)
                {
                    string hashedEnteredPassword = HashUtil.ComputeSHA256Hash(user.Password);
                    if (loginUser.IsAuthenticateUser(hashedEnteredPassword))
                    {
                        bool isRememberMe = Request.Form["RememberMe"] == "true";
                        LoginUserInfo userInfo = new LoginUserInfo
                        {
                            UserID = loginUser.UserID,
                            LoggedInTime = DateTime.Now,
                            RememberMe = isRememberMe,
                            UserType = loginUser.UserType.UserTypeName
                        };
                        SessionUtil.SetLoginUserInfo(HttpContext, userInfo);
                        return RedirectToAction("SearchVehicle", "VehicleData");
                    }
                }
                Utility.AlertMessage(this, "Invalid Username or Password.", "alert-danger");
                return View("Login");
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "SQl Connection Error.Please refresh browser.", "alert-danger");
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult RemoveOneTapLogin()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Logout()
        {
            LoginUserInfo loginUserInfo = SessionUtil.GetLoginUserInfo(HttpContext);
            var loginUser = factoryBuilder.CreateUserService().FindUserByUserName(loginUserInfo.UserID);

            if (loginUserInfo.RememberMe)
            {
                SessionUtil.SetLoginUserInfo(HttpContext, loginUserInfo);
                return RedirectToAction("Index", "Login");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}
