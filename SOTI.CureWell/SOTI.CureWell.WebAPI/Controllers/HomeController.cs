using SOTI.CureWell.DAL;
using SOTI.CureWell.DAL.Interfaces;
using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SOTI.CureWell.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Home")]
    /// <summary>
    /// Controller for handling home-related actions.
    /// </summary>
    public class HomeController : ApiController
    {
        private readonly IDoctor _doctor = null;
        private readonly ISurgery _surgery = null;
        private readonly ISpecialization _specialization = null;
        private readonly IDoctorSpecialization _doctorSpecialization = null;

        /// <summary>
        /// Constructor to initialize the class variables
        /// </summary>
        /// <param name="doctor"></param>
        /// <param name="surgery"></param>
        /// <param name="specialization"></param>
        /// <param name="doctorSpecialization"></param>
        public HomeController(IDoctor doctor,ISurgery surgery,ISpecialization specialization,IDoctorSpecialization doctorSpecialization)
        {
            _surgery = surgery;
            _doctor = doctor;
            _specialization = specialization;
            _doctorSpecialization = doctorSpecialization;
        }

        /// <summary>
        /// Controller to get all the doctors
        /// </summary>
        [HttpGet]
        [Route("AllDoctors")]
        public IHttpActionResult GetDoctors()
        {
            var dt = _doctor.GetAllDoctors();
            if (dt == null)
            {
                return BadRequest();
            }
            return Ok(dt);
        }

        /// <summary>
        /// Controller to add doctor in the doctor table
        /// </summary>
        /// <param name="doctor"></param>
       
        [HttpPost]
        [Route("AddDoctor")]
        public IHttpActionResult AddDoctor([FromBody] Doctor doctor)
        {
            var dt = _doctor.AddDoctor(doctor.DoctorName);
            if (dt == true)
            {
                return Ok(true);
            }
            return BadRequest();
        }
        /// <summary>
        /// Controller to get all the surgeries performed today 
        /// </summary>
        [HttpGet]

        [Route("AllSurgeries")]

        public IHttpActionResult GetAllSurgeryTypeForToday()
        {
            //CureWellRepository surgery = new CureWellRepository();
            var dt = _surgery.GetAllSurgeryTypeForToday();
            if (dt == null)
            {
                return BadRequest();
            }
            return Ok(dt);
        }

        /// <summary>
        /// Controller to get all the specializations
        /// </summary>
        [HttpGet]
        [Route("AllSpecializations")]
        public IHttpActionResult GetSpecializations()
        {
            //return Json(_specializationDetails.GetAllSpecialization()); 
            var res = _specialization.GetAllSpecialization();
            if (res == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }

        /// <summary>
        /// Controller to delete the doctor
        /// </summary>
        /// <param name="doctorId"></param>
        [HttpDelete]
        [Route("DeleteDoctor/{doctorId}")]
        public IHttpActionResult DeleteDoctor([FromUri] int doctorId)
        {
            var dt = _doctor.DeleteDoctor(doctorId);
            if (dt == true)
            {
                return Ok(true);
            }
            return BadRequest();
        }
        /// <summary>
        /// Controller to get all the doctors having a particular specialization
        /// </summary>
        /// <param name="specialization"></param>

        [HttpGet]
        [Route("DoctorBySpecialization/{specialization}")]
        public IHttpActionResult GetDoctorsBySpecialization(string specialization)
        {
            var dt = _doctorSpecialization.GetDoctorsBySpecialization(specialization);
            if (dt == null)
            {
                return BadRequest();
            }
            return Ok(dt);
        }
        /// <summary>
        /// Controller to update the doctor details
        /// </summary>
        /// <param name="doctor"></param>

        [HttpPut]
        [Route("UpdateDoctorDetails")]
        public IHttpActionResult UpdateDoctorDetails([FromBody] Doctor doctor)
        {
            var dt = _doctor.UpdateDoctorDetails(doctor);
            if (dt == true)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        /// <summary>
        /// Controller to update details of a particular surgery
        /// </summary>
        /// <param name="surgery"></param>
        [HttpPut]
        [Route("UpdateSurgery")]
        public IHttpActionResult UpdateSurgery([FromBody] Surgery surgery)
        {
            var dt = _surgery.UpdateSurgery(surgery);
            if (dt == true)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet]
        [Route("GetSurgeryById/{surgeryId}")]
        public IHttpActionResult GetSurgeryById([FromUri] int surgeryId)
        {
            var res = _surgery.GetSurgeryById(surgeryId);
            if (res == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }

}
