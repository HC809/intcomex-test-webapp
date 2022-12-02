using intcomex_test_webapp.BLL.DTOs;
using intcomex_test_webapp.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace intcomex_test_webapp.Controllers
{
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IContactTypeService _contactTypeService;

        public UsersController(IUserService userService, IContactTypeService contactTypeService)
        {
            _userService = userService;
            _contactTypeService = contactTypeService;
        }

        public IActionResult Index() => View();

        public async Task<JsonResult> GetAll() => Json(new { data = await _userService.GetAll() });

        public async Task<IActionResult> Save(UserDTO user) => Ok(await _userService.Create(user));

        public async Task<IActionResult> Delete(int id) => Ok(await _userService.Delete(id));

        public async Task<PartialViewResult> FormPartialView(int userNo)
        {
            var contactTypes = await _contactTypeService.GetAll();
            ViewBag.ContactTypes = contactTypes != null ? contactTypes.Select(x => new SelectListItem { Value = x.ContactTypeNo.ToString(), Text = x.ContactTypeName }) : new List<SelectListItem>();

            var user = new UserDTO();

            if (userNo != 0)
                user = await _userService.GetById(userNo);

            return PartialView("_Form", user);
        }
    }
}
