using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TemperatureWebSite.Models;

namespace TemperatureWebSite.Controllers
{
    public class TemperatureController : Controller
    {
        public HttpClient Client { get; set; }

        // dependency injection
        public TemperatureController(HttpClient client)
        {
            Client = client;
        }

        // GET: Temperature
        public async Task<ActionResult> Index()
        {
            // send "GET api/Temperature" to service, get headers of response
            HttpResponseMessage response = await Client.GetAsync("https://localhost:44365/api/temperature");

            // (if status code is not 200-299 (for success))
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }

            // get the whole response body (second await)
            var responseBody = await response.Content.ReadAsStringAsync();

            // this is a string, so it must be deserialized into a C# object.
            // we could use DataContractSerializer, .NET built-in, but it's more awkward
            // than the third-party Json.NET aka Newtonsoft JSON.
            List<TemperatureRecord> temperatures = JsonConvert.DeserializeObject<List<TemperatureRecord>>(responseBody);

            return View(temperatures);
        }

        // GET: Temperature/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await Client.GetAsync("https://localhost:44365/api/temperature/" + id.ToString());

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            TemperatureRecord temperature = JsonConvert.DeserializeObject<TemperatureRecord>(responseBody);

            return View(temperature);
        }

        // GET: Temperature/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Temperature/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TemperatureRecord temperature)
        {
            try
            {
                // TODO: Add insert logic here
                HttpResponseMessage response = await Client.PostAsJsonAsync("https://localhost:44365/api/temperature", temperature);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Temperature/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await Client.GetAsync("https://localhost:44365/api/temperature/" + id.ToString());

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            TemperatureRecord temperature = JsonConvert.DeserializeObject<TemperatureRecord>(responseBody);

            return View(temperature);
        }

        // POST: Temperature/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TemperatureRecord temperature)
        {
            try
            {
                // TODO: Add update logic here
                string idStr = temperature.Id.ToString();
                HttpResponseMessage response = await Client.PutAsJsonAsync("https://localhost:44365/api/temperature/"+idStr, temperature);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Temperature/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            HttpResponseMessage response = await Client.GetAsync("https://localhost:44365/api/temperature/" + id.ToString());

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            TemperatureRecord temperature = JsonConvert.DeserializeObject<TemperatureRecord>(responseBody);

            return View(response);
        }

        // POST: Temperature/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}