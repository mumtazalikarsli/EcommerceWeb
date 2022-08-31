using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIWithCoreMvc.ApiServices.Interfaces;

namespace WebAPIWithCoreMvc.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class UserController : Controller
    {
        private IUserApiService _userApiService;
        public UserController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userApiService.GetListAsync());
        }
    }
}
