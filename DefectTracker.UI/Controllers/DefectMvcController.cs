using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using DefectTracker.Models;

namespace DefectTracker.UI.Controllers
{
    public class DefectMvcController : Controller
    {
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    IEnumerable<Defect> defectList = null;
                    HttpResponseMessage response = await client.GetAsync("http://localhost:56003/api/Defect/");
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsAsync<IList<Defect>>();
                        readTask.Wait();

                        defectList = readTask.Result;
                    }
                    else
                    {
                        defectList = Enumerable.Empty<Defect>();
                    }
                    return View(defectList);
                }
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        public async Task<ActionResult> CreateAsync()
        {
            try
            {
                await GetUserListAsync();
                await GetStatusListAsync();
            }
            catch
            {
                return View("ExceptionPage");
            }
            return View();
        }

        public async Task<ActionResult> CreateDefectAsync(Defect defect)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:56003/api/Defect/", defect);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        public async Task<ActionResult> EditAsync(int id)
        {
            Defect defect = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56003/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("Defect?id=" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Defect>();
                        readTask.Wait();

                        defect = readTask.Result;
                    }
                    await GetUserListAsync();
                    await GetStatusListAsync();
                }

                return View(defect);
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        [HttpPost]
        public ActionResult Edit(Defect defect)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56003/api/Defect");

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<Defect>("Defect", defect);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
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
                    var deleteTask = client.DeleteAsync("Defect/" + id.ToString());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "The record was successfully deleted";
                        return RedirectToAction("Index");
                    }
                }
                TempData["Message"] = "The record was successfully deleted";
                return RedirectToAction("Index");
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        public async Task GetUserListAsync()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<Users> usersList = null;
                HttpResponseMessage response = await client.GetAsync("http://localhost:56003/api/Users/");
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<IList<Users>>();
                    readTask.Wait();

                    usersList = readTask.Result;
                }
                else
                {
                    usersList = Enumerable.Empty<Users>();
                }
                ViewBag.UsersList = usersList.ToDropDownList(0);
            }
        }

        public async Task GetStatusListAsync()
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
                ViewBag.DefectStatusList = defectStatusList.ToDropDownList(0);
            }
        }
    }
}