using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DefectTracker.DataAccessLayer;
using DefectTracker.Models;

namespace DefectTracker.UI.Controllers
{
    namespace DefectTracker.Controllers
    {
        public class DefectStatusController : ApiController
        {
            // GET: api/DefectStatus
            public IHttpActionResult Get()
            {
                try
                {
                    var defectstatusQueryObject = new DefectStatusQueryObject();
                    IEnumerable<DefectStatus> defectStatusList = defectstatusQueryObject.GetDefectStatusList();
                    if (defectStatusList.Count().Equals(0)) return NotFound();
                    return Ok(defectStatusList);
                }
                catch
                {
                    return Redirect("ErrorPage");
                }
            }

            // GET: api/DefectStatus/5
            public IHttpActionResult Get(int id)
            {
                var defectstatusQueryObject = new DefectStatusQueryObject();
                var defectStatus = defectstatusQueryObject.GetDefectStatus(id);
                return Ok(defectStatus);
            }

            // POST: api/DefectStatus
            public void Post([FromBody]string value)
            {
                var defectstatusQueryObject = new DefectStatusQueryObject();
                var defectStatus = new DefectStatus() { Status = value };
                defectstatusQueryObject.AddDefectStatus(defectStatus);
            }

            // PUT: api/DefectStatus/5
            public IHttpActionResult Put(DefectStatus defectstatus)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid data");
                else
                {
                    var defectstatusQueryObject = new DefectStatusQueryObject();
                    defectstatusQueryObject.EditDefectStatus(defectstatus);
                    return Ok(defectstatus);
                }
            }

            // DELETE: api/DefectStatus/5
            public IHttpActionResult Delete(int id)
            {
                try
                {
                    if (id <= 0)
                        return BadRequest("Not a valid defect status id");
                    else
                    {
                        var defectstatusQueryObject = new DefectStatusQueryObject();
                        defectstatusQueryObject.DeleteDefectStatus(id);
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