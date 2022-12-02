using intcomex_test_webapp.DAL;
using Microsoft.AspNetCore.Mvc;

namespace intcomex_test_webapp.Controllers
{
    public class EmployeesController : Controller
    {
        private IDapperSqlService _dapperService;

        public EmployeesController(IDapperSqlService dapperService)
        {
            _dapperService = dapperService;
        }

        public IActionResult Index() => View();

        public async Task<JsonResult> GetEmployeesTotalBorn() => Json(new { data = await _dapperService.GetEmployeesTotalBorn() });
    }
}
