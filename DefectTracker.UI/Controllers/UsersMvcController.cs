using DefectTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DefectTracker.UI.Controllers
{
    public class UsersMvcController : Controller
    {
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            try
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
                    return View(usersList);
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

        public async Task<ActionResult> CreateAsync(Users users)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:56003/api/Users/", users.Name);
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
            Users users = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56003/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("Users?id=" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Users>();
                        readTask.Wait();

                        users = readTask.Result;
                    }
                }

                return View(users);
            }
            catch
            {
                return View("ExceptionPage");
            }
        }

        [HttpPost]
        public ActionResult Edit(Users users)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56003/api/Users");

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<Users>("Users", users);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(users);
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
                    var deleteTask = client.DeleteAsync("Users/" + id.ToString());
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
    }
}