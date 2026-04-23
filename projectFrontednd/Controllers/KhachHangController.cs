using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectFrontednd.DAO;
using System.Text;
using System.Text.Json;

namespace projectFrontednd.Controllers
{
    public class KhachHangController : Controller
    {

        private string GetGivenApiBaseURL()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string baseUrl = config["GivenAPIBaseUrl"];
            return baseUrl;
        }
        private readonly HttpClient _httpClient = new HttpClient();

        // GET: KhachHangController
        public async Task<ActionResult> Index()
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/KhachHang");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            List<KhachHangDTO> user =
                JsonSerializer.Deserialize<List<KhachHangDTO>>(json, option);
            return View(user);
        }

        // GET: KhachHangController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/KhachHang/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            KhachHangDTO user =
                JsonSerializer.Deserialize<KhachHangDTO>(json, option);
            return View(user);
        }

        // GET: KhachHangController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("maKhachHang")] KhachHangDTO user)
        {
            try
            {
                string jsonProduct = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                string baseUrl = GetGivenApiBaseURL();
                HttpResponseMessage response = await _httpClient.PostAsync($"{baseUrl}/api/KhachHang", content);

                string result = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { WriteIndented = true };
                KhachHangDTO addUser =
                    JsonSerializer.Deserialize<KhachHangDTO>(result, option);

                //return RedirectToAction(nameof(Index));
                return View("Details", addUser);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: KhachHangController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/KhachHang/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            KhachHangDTO user =
                JsonSerializer.Deserialize<KhachHangDTO>(json, option);
            return View(user);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("maKhachHang,hoTen,diaChi,soDienThoai")] KhachHangDTO user)
        {
            try
            {
                var updateProduct = new
                {
                    maKhachHang = user.maKhachHang,
                    hoTen = user.hoTen,
                    diaChi = user.diaChi,
                    soDienThoai = user.soDienThoai,
                };
                string jsonProduct = JsonSerializer.Serialize(updateProduct);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                string baseUrl = GetGivenApiBaseURL();
                HttpResponseMessage response = await _httpClient.PutAsync($"{baseUrl}/api/KhachHang/{id}", content);

                string result = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { WriteIndented = true };
                KhachHangDTO updatedUser =
                    JsonSerializer.Deserialize<KhachHangDTO>(result, option);

                //return RedirectToAction(nameof(Index));
                return View("Details", updatedUser);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
