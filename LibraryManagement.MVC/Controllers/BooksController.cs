using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using LibraryManagement.MVC.Models;

namespace LibraryManagement.MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7087/api/books";

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // GET: /Books (List)
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var books = JsonSerializer.Deserialize<List<BookViewModel>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(books ?? new List<BookViewModel>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Failed to load books: " + ex.Message;
            }
            return View(new List<BookViewModel>());
        }

        // GET: /Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var book = JsonSerializer.Deserialize<BookViewModel>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(book);
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: /Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Books/Create
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel model)
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
                    ModelState.AddModelError("", "Failed to create book: " + ex.Message);
                }
            }
            return View(model);
        }

        // GET: /Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var book = JsonSerializer.Deserialize<BookViewModel>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var model = new BookUpdateViewModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        ISBN = book.ISBN,
                        Status = book.Status
                    };
                    return View(model);
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: /Books/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookUpdateViewModel model)
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
                    ModelState.AddModelError("", "Failed to update book: " + ex.Message);
                }
            }
            return View(model);
        }

        // POST: /Books/Delete/5
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
