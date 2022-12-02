using intcomex_test_webapp.DAL;
using Microsoft.AspNetCore.Mvc;

namespace intcomex_test_webapp.Controllers
{
    public class DepartmentsController : Controller
    {
        private IDapperSqlService _dapperService;

        public DepartmentsController(IDapperSqlService dapperService)
        {
            _dapperService = dapperService;
        }

        public IActionResult Index() => View();

        public async Task<JsonResult> GetDepartmentsSalaries() => Json(new { data = await _dapperService.GetDepartmentsTotalSalaries() });
    }
}
