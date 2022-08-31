using Entities.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using WebAPIWithCoreMvc.ViewModels;

namespace WebAPIWithCoreMvc.Controllers
{
    public class UsersController : Controller
    {
        #region defines
        private readonly HttpClient _httpclient;
        private string url = "https://localhost:44348/api/";
        #endregion defines

        #region Constructor
        public UsersController(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }
        #endregion
        public async Task<IActionResult> Index()
        {
            var users = await _httpclient.GetFromJsonAsync<List<UserDetailDto>>(url + "controller/GetList");
            return View(users);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.GenderList = GenderFill();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            UserAddDto userAddDto = new UserAddDto()
            {
                FirstName = userAddViewModel.FirstName,
                LastName = userAddViewModel.LastName,
                Gender = userAddViewModel.GenderID == 1 ? true : false,
                Address = userAddViewModel.Address,
                DateOfBirth = userAddViewModel.DateOfBirth,
                Email = userAddViewModel.Email,
                Password = userAddViewModel.Password,
                UserName = userAddViewModel.UserName,
                PhoneNumber = userAddViewModel.PhoneNumber,
            };
            HttpResponseMessage responseMessage = await _httpclient.PostAsJsonAsync(url + "controller/Add", userAddDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _httpclient.GetFromJsonAsync<UserDto>(url + "controller/GetById/" + id);
            UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel()
            {
                FirstName = user.FirstName,
                GenderID = user.Gender == true ? 1 : 2,
                Address = user.Address,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                UserName = user.UserName,
            };
            ViewBag.GenderList = GenderFill();
            return View(userUpdateViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _httpclient.GetFromJsonAsync<UserDto>(url + "controller/GetById/" + id);
            UserDeleteViewModel userDeleteViewModel = new UserDeleteViewModel()
            {
                FirstName = user.FirstName,
                GenderName = user.Gender == true ? "Erkek" : "Kadın",
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
            };
            ViewBag.GenderList = GenderFill();
            return View(userDeleteViewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var IsDelete = await _httpclient.DeleteAsync(url + "controller/Delete/" + id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UserUpdateViewModel userUpdateViewModel)
        {
            UserUpdateDto userUpdateDto = new UserUpdateDto()
            {
                FirstName = userUpdateViewModel.FirstName,
                Gender = userUpdateViewModel.GenderID == 1 ? true : false,
                Address = userUpdateViewModel.Address,
                PhoneNumber = userUpdateViewModel.PhoneNumber,
                LastName = userUpdateViewModel.LastName,
                DateOfBirth = userUpdateViewModel.DateOfBirth,
                Email = userUpdateViewModel.Email,
                Password = userUpdateViewModel.Password,
                UserName = userUpdateViewModel.UserName,
                Id = id,
            };
            HttpResponseMessage httpResponseMessage = await _httpclient.PutAsJsonAsync(url + "controller/Update", userUpdateDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        private List<Gender> GenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender() { Id = 1, GenderName = "Erkek" });
            genders.Add(new Gender() { Id = 2, GenderName = "Kadın" });
            return genders;
        }

        private class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }
    }
}
