using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using LibraryManagement.MVC.Models;

namespace LibraryManagement.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7087/api/users";

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // GET: /Users
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var users = JsonSerializer.Deserialize<List<UserViewModel>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(users ?? new List<UserViewModel>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Failed to load users: " + ex.Message;
            }
            return View(new List<UserViewModel>());
        }

        // GET: /Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<UserViewModel>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(user);
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: /Users/Create
        public IActionResult Create()
        {
            ViewBag.Roles = new[] { "Admin", "Librarian", "Member" };
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonSerializer.Serialize(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_apiUrl, content);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create user: " + ex.Message);
                }
            }
            ViewBag.Roles = new[] { "Admin", "Librarian", "Member" };
            return View(model);
        }

        // GET: /Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<UserViewModel>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var model = new UserUpdateViewModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Role = user.Role,
                        Phone = user.Phone,
                        JoinedDate = user.JoinedDate
                    };
                    ViewBag.Roles = new[] { "Admin", "Librarian", "Member" };
                    return View(model);
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: /Users/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserUpdateViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonSerializer.Serialize(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync($"{_apiUrl}/{id}", content);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to update user: " + ex.Message);
                }
            }
            ViewBag.Roles = new[] { "Admin", "Librarian", "Member" };
            return View(model);
        }

        // POST: /Users/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                    return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            return Json(new { success = false });
        }

        
    }
}
