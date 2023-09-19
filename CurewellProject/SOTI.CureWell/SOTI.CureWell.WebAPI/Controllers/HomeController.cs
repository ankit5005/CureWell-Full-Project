using SOTI.CureWell.DAL;
using SOTI.CureWell.DAL.Model;
using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOTI.CureWell.WebAPI.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private readonly ICureWell _doctorDetail = null;

        public HomeController()
        {
            _doctorDetail = new CureWellRepository();
        }



        [HttpPut]
        [Route("updateSurgery/{id}")]
        public IHttpActionResult UpdateSurgery([FromUri]int id, [FromBody] Surgery sObj)
        {
            if (id != sObj.SurgeryId) return BadRequest();
            var ds = _doctorDetail.UpdateSurgery(sObj);
            if (ds)
            {
                return Ok("Updated Successfully");
            }

            return BadRequest();

        }

        [HttpPut]
        [Route("update/{id}")]
        public IHttpActionResult UpdateDoctorDetails([FromUri] int id , [FromBody] Doctor obj)
        {

            if (id != obj.DoctorId) return BadRequest();

            var dt = _doctorDetail.UpdateDoctorDetails(obj);

            if (dt)
            {
                return Ok("Updated");
            }

            return BadRequest();
        }
    }
}
