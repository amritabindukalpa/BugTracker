using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DefectTracker.DataAccessLayer;
using DefectTracker.Models;

namespace DefectTracker.UI.Controllers
{
    namespace DefectTracker.Controllers
    {
        public class UsersController : ApiController
        {
            // GET: api/Users
            public IHttpActionResult Get()
            {
                try
                {
                    var usersQueryObject = new UsersQueryObject();
                    IEnumerable<Users> usersList = usersQueryObject.GetUsersList();
                    var enumerable = usersList.ToList();
                    if (enumerable.Count().Equals(0)) return NotFound();
                    return Ok(enumerable);
                }
                catch
                {
                    return Redirect("ErrorPage");
                }
            }

            // GET: api/Users/5
            public IHttpActionResult Get(int id)
            {
                var usersQueryObject = new UsersQueryObject();
                var users = usersQueryObject.GetUsers(id);
                return Ok(users);
            }

            // POST: api/Users
            public void Post([FromBody]string value)
            {
                var usersQueryObject = new UsersQueryObject();
                var users = new Users() { Name = value };
                usersQueryObject.AddUsers(users);
            }

            // PUT: api/Users/5
            public IHttpActionResult Put(Users users)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid data");
                else
                {
                    var usersQueryObject = new UsersQueryObject();
                    usersQueryObject.EditUsers(users);
                    return Ok(users);
                }
            }

            // DELETE: api/Users/5
            public IHttpActionResult Delete(int id)
            {
                try
                {
                    if (id <= 0)
                        return BadRequest("Not a valid user id");
                    else
                    {
                        var usersQueryObject = new UsersQueryObject();
                        usersQueryObject.DeleteUsers(id);
                        return Ok();
                    }
                }
                catch
                {
                    return Redirect("ErrorPage");
                }
            }
        }
    }
}