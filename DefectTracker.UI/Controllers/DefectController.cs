using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DefectTracker.DataAccessLayer;
using DefectTracker.Models;

namespace DefectTracker.UI.Controllers
{
    namespace DefectTracker.Controllers
    {
        public class DefectController : ApiController
        {
            // GET: api/Defect
            public IHttpActionResult Get()
            {
                try
                {
                    var defectQueryObject = new DefectQueryObject();
                    IEnumerable<Defect> defectList = defectQueryObject.GetDefectList();
                    if (defectList.Count().Equals(0)) return NotFound();
                    return Ok(defectList);
                }
                catch
                {
                    return Redirect("ErrorPage");
                }
            }

            // GET: api/Defect/5
            public IHttpActionResult Get(int id)
            {
                var defectQueryObject = new DefectQueryObject();
                var defect = DefectQueryObject.GetDefect(id);
                return Ok(defect);
            }

            // POST: api/Defect
            public void Post([FromBody]Defect defect)
            {
                var defectQueryObject = new DefectQueryObject();
                DefectQueryObject.AddDefect(defect);
            }

            // PUT: api/Defect/5
            public IHttpActionResult Put(Defect defect)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid data");
                else
                {
                    var defectQueryObject = new DefectQueryObject();
                    defectQueryObject.EditDefect(defect);
                    return Ok(defect);
                }
            }

            // DELETE: api/Defect/5
            public IHttpActionResult Delete(int id)
            {
                try
                {
                    if (id <= 0)
                        return BadRequest("Not a valid defect status id");
                    else
                    {
                        var defectQueryObject = new DefectQueryObject();
                        defectQueryObject.DeleteDefect(id);
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