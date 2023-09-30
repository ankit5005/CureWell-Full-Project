using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL.Models
{
    public class BookAppointment
    {
        public int AppointmentID { get; set; }
        public string EmailId { get; set; }
        public string SpecializationCode { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PatientName { get; set; }
        public string Description { get; set; }
    }
}