using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace projectFrontednd.Controllers
{
    public class DoanhThuController : Controller
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

        // GET: DoanhThuController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DoanhThuController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoanhThuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoanhThuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoanhThuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoanhThuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoanhThuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoanhThuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
