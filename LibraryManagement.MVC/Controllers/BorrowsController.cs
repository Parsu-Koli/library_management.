using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using LibraryManagement.MVC.Models;

namespace LibraryManagement.MVC.Controllers
{
    public class BorrowsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7087/api/borrows";

        public BorrowsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // GET: /Borrows (List All)
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var borrows = JsonSerializer.Deserialize<List<BorrowViewModel>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(borrows ?? new List<BorrowViewModel>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Failed to load borrows: " + ex.Message;
            }
            return View(new List<BorrowViewModel>());
        }

        // GET: /Borrows/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var borrow = JsonSerializer.Deserialize<BorrowViewModel>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(borrow);
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: /Borrows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Borrows/Create
        [HttpPost]
        public async Task<IActionResult> Create(BorrowCreateViewModel model)
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
                    ModelState.AddModelError("", "Failed to create borrow: " + ex.Message);
                }
            }
            return View(model);
        }

        // POST: /Borrows/Return/5
        public async Task<IActionResult> Return(int id)
        {
            try
            {
                var response = await _httpClient.PutAsync($"{_apiUrl}/{id}/return", null);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to return book: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
