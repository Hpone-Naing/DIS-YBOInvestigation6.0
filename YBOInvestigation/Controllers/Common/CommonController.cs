using Microsoft.AspNetCore.Mvc;
using YBOInvestigation.Factories;

namespace YBOInvestigation.Controllers.Common
{
    public class CommonController : Controller
    {
        private readonly IConfiguration _configuration;

        public CommonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JsonResult GetUrl()
        {
            string testUrl = _configuration["AppUrls:TestUrl"];
            return Json(new { TestUrl = testUrl });
        }
    }
}
