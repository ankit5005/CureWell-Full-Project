using SOTI.CureWell.DAL;
using SOTI.CureWell.DAL.Model;
using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using static SOTI.CureWell.DAL.ISurgery;

namespace SOTI.CureWell.WebAPI.Controllers
{
    [EnableCors("*","*","*")]
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private readonly IDoctor _doctorDetail = null;
        private readonly ISurgery _SurgeryDetail = null;

        public HomeController()
        {
            _doctorDetail = new CureWellRepository();
            _SurgeryDetail = new CureWellRepository();
        }
        [HttpGet]
        [Route ("GetDoctorById")]
            public IHttpActionResult GetDoctorById([FromBody]string SpecializationCode)
      {
            var specialization = _doctorDetail.GetDoctorBySpecialization(SpecializationCode);
            if (specialization == null)
            {
                return BadRequest();
            }
            return Ok(specialization);
        }
        
        
        [HttpGet]
        [Route("SurgeryToday")]
        public IHttpActionResult GetAllSurgeryTypeForToday()
        {
            var specialization = _SurgeryDetail.GetAllSurgeryTypeForToday();
            if (specialization == null)
            {
                return BadRequest();
            }
            return Ok(specialization);
        }
        [HttpGet]
        [Route ("GetAllSpecializations")]
        public IHttpActionResult GetAllSpecialization()
        {
            var specialization = _SurgeryDetail.GetAllSpecializations();
            if (specialization == null)
            {
                return BadRequest();
            }
            return Ok(specialization);
        }

        [HttpGet]
        [Route("GetAllDoctors")]
        public IHttpActionResult GetAllDoctors()
        {
            var dt = _doctorDetail.GetAllDoctor();
            if (dt == null)
            {
                return BadRequest();
            }
            return Ok(dt);
        }
        [HttpPost]
        [Route("AddDoctor")]
        public IHttpActionResult AddDoctor([FromBody ] Doctor dObj)
        {
            var ds = _doctorDetail.AddDoctor(dObj);
            if (ds)
            {
                return Ok("Add Successfully");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("updateSurgery/{id}")]
        public IHttpActionResult UpdateSurgery([FromUri]int id, [FromBody] Surgery sObj)
        {
            if (id != sObj.SurgeryId) return BadRequest();
            var ds = _SurgeryDetail.UpdateSurgery(sObj);
            if (ds)
            {
                return Ok("Updated Successfully");
            }

            return BadRequest();

        }

        [HttpPut]
        [Route("update")]
        public IHttpActionResult UpdateDoctorDetails( [FromBody] Doctor obj)
        {
            var dt = _doctorDetail.UpdateDoctorDetails(obj);

            if (dt)
            {
                return Ok("Updated");
            }

            return BadRequest();
        }
    }
}
