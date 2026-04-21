using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectFrontednd.DAO;
using projectFrontednd.Models;
using System.Text;
using System.Text.Json;

namespace projectFrontednd.Controllers
{
    public class NhanVienController : Controller
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

        // GET: NhanVienController
        public async Task<ActionResult> Index()
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/NhanVien");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            List<NhanVienDAO> products =
                JsonSerializer.Deserialize<List<NhanVienDAO>>(json, option);
            return View(products);
        }

        // GET: NhanVienController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/NhanVien/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            NhanVienDAO product =
                JsonSerializer.Deserialize<NhanVienDAO>(json, option);
            return View(product);
        }

        // GET: NhanVienController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("maNhanVien")] NhanVienDAO product)
        {
            try
            {
                string jsonProduct = JsonSerializer.Serialize(product);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                string baseUrl = GetGivenApiBaseURL();
                HttpResponseMessage response = await _httpClient.PostAsync($"{baseUrl}/api/NhanVien", content);

                string result = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { WriteIndented = true };
                NhanVienDAO addedProduct =
                    JsonSerializer.Deserialize<NhanVienDAO>(result, option);

                //return RedirectToAction(nameof(Index));
                return View("Details", addedProduct);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: NhanVienController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/NhanVien/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            NhanVienDAO product =
                JsonSerializer.Deserialize<NhanVienDAO>(json, option);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("maNhanVien,hoTen,diaChi,soDienThoai")] NhanVienDAO nhanvien)
        {
            try
            {
                var updateProduct = new
                {
                    maNhanVien = nhanvien.maNhanVien,
                    hoTen = nhanvien.hoTen,
                    diaChi = nhanvien.diaChi,
                    soDienThoai = nhanvien.soDienThoai,
                };
                string jsonProduct = JsonSerializer.Serialize(updateProduct);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                string baseUrl = GetGivenApiBaseURL();
                HttpResponseMessage response = await _httpClient.PutAsync($"{baseUrl}/api/NhanVien/{id}", content);

                string result = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { WriteIndented = true };
                NhanVienDAO updatedProduct =
                    JsonSerializer.Deserialize<NhanVienDAO>(result, option);

                //return RedirectToAction(nameof(Index));
                return View("Details", updatedProduct);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: NhanVienController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            string baseUrl = GetGivenApiBaseURL();
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{baseUrl}/api/NhanVien/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { WriteIndented = true };
            NhanVienDAO product =
                JsonSerializer.Deserialize<NhanVienDAO>(json, option);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<string> DeleteConfirmed(int id)
        {
            try
            {
                string baseUrl = GetGivenApiBaseURL();
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{baseUrl}/api/NhanVien/{id}");
                string result = await response.Content.ReadAsStringAsync();

                //return RedirectToAction(nameof(Index));
                return result;
            }
            catch (Exception ex)
            {
                //return View();
                return ex.Message;
            }
        }
    }
}
