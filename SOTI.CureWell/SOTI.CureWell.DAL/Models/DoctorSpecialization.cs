using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL.Models
{
    /// <summary>
    /// Represents the doctor specialization
    /// </summary>
    public class DoctorSpecialization
    {
        /// <summary>
        /// Gets or sets doctorId
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// Gets or sets specialization code
        /// </summary>
        public string SpecializationCode { get; set; }
        /// <summary>
        /// Gets or sets specialization date
        /// </summary>
        public DateTime SpecializationDate { get; set; }


    }
}
