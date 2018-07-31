using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using DefectTracker.Models;

namespace DefectTracker.UI.Controllers
{
    public class DefectStatusMvcController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    IEnumerable<DefectStatus> defectStatusList = null;
                    HttpResponseMessage response = await client.GetAsync("http://localhost:56003/api/defectstatus/");
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<IList<DefectStatus>>();
                        readTask.Wait();

                        defectStatusList = readTask.Result;
                    }
                    else
                    {
                        defectStatusList = Enumerable.Empty<DefectStatus>();
                    }
                    return View(defectStatusList);
                }
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> CreateAsync(DefectStatus defectStatus)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:56003/api/defectstatus/", defectStatus.Status);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        public ActionResult Edit(int id)
        {
            DefectStatus defectStatus = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56003/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("defectstatus?id=" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<DefectStatus>();
                        readTask.Wait();

                        defectStatus = readTask.Result;
                    }
                }

                return View(defectStatus);
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        [HttpPost]
        public ActionResult Edit(DefectStatus defectStatus)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56003/api/defectstatus");

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<DefectStatus>("defectStatus", defectStatus);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(defectStatus);
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56003/api/");

                    //HTTP DELETE
                    var deleteTask = client.DeleteAsync("Defectstatus/" + id.ToString());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "The record was successfully deleted";
                        return RedirectToAction("Index");
                    }
                }
                TempData["Message"] = "There was an error in deleting the record.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ExceptionPage");
            }
        }
    }
}