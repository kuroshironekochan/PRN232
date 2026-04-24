using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectFrontednd.DAO;
using System.Text;
using System.Text.Json;

namespace projectFrontednd.Controllers
{
    public class ThueXeController : Controller
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

        // GET: ThueXeController
        public async Task<ActionResult> Index()
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/ThueXeTheoGio");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            List<ThueXeTheoGioDto> user =
                JsonSerializer.Deserialize<List<ThueXeTheoGioDto>>(json, option);
            return View(user);
        }

        // GET: ThueXeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/ThueXeTheoGio/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            ThueXeTheoGioDto user =
                JsonSerializer.Deserialize<ThueXeTheoGioDto>(json, option);
            return View(user);
        }

        // GET: ThueXeController/Create
        // GET: NhanVienController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("maThue")] ThueXeTheoGioDto product)
        {
            try
            {
                string jsonProduct = JsonSerializer.Serialize(product);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                string baseUrl = GetGivenApiBaseURL();
                HttpResponseMessage response = await _httpClient.PostAsync($"{baseUrl}/api/ThueXeTheoGio", content);

                string result = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { WriteIndented = true };
                ThueXeTheoGioDto addedProduct =
                    JsonSerializer.Deserialize<ThueXeTheoGioDto>(result, option);

                //return RedirectToAction(nameof(Index));
                return View("Details", addedProduct);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: ThueXeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/ThueXeTheoGio/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            ThueXeTheoGioDto user =
                JsonSerializer.Deserialize<ThueXeTheoGioDto>(json, option);
            return View(user);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("maThue,maKhachHang,maXe,maNhanVien,thoiGianBatDau,thoiGianKetThuc")] ThueXeTheoGioDto user)
        {
            try
            {
                var updateProduct = new
                {
                    maThue = user.maThue,
                    maKhachHang = user.maKhachHang,
                    maXe = user.maXe,
                    maNhanVien = user.maNhanVien,
                    thoiGianBatDau = user.thoiGianBatDau,
                    thoiGianKetThuc = user.thoiGianKetThuc,
                };
                string jsonProduct = JsonSerializer.Serialize(updateProduct);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                string baseUrl = GetGivenApiBaseURL();
                HttpResponseMessage response = await _httpClient.PutAsync($"{baseUrl}/api/ThueXeTheoGio/{id}", content);

                string result = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { WriteIndented = true };
                ThueXeTheoGioDto updatedUser =
                    JsonSerializer.Deserialize<ThueXeTheoGioDto>(result, option);

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
